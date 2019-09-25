using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

// script for setting up ARKit for 3D head tracking purposes

public class HeadTrackManager : MonoBehaviour {

	[SerializeField]
	private GameObject headCenter;

	//public Light keylight;
	public CameraManager camManager;

	private UnityARSessionNativeInterface m_session;

	Dictionary<string, float> currentBlendShapes;

	public float leftEyeClosed = 0f;
	public float rightEyeClosed = 0f;

	public enum OpenEye { Left, Right };
	[System.NonSerialized]
	public OpenEye openEye = OpenEye.Right;
	bool autoEye = false;

	public string eyeInfoText; // little status, which eye is being tracked, auto or not
	public float IPD = 64f; // inter pupil distance (mm)
	public float EyeHeight = 32f; // eye height from head anchor (mm)

	public string ARError;
    public GameObject RequireIPhoneXPanel;

    public void SetIPD( float value ) {
		IPD = value;
	}
		
	public void SetEyeHeight( float value ) {
		EyeHeight = value;
	}


	// Use this for initialization
	public void Start () {

		// first try to get camera acess
		//yield return RequestCamera ();

		ARError = null;

		m_session = UnityARSessionNativeInterface.GetARSessionNativeInterface();

		UnityARSessionNativeInterface.ARSessionFailedEvent += CatchARSessionFailed;

		Application.targetFrameRate = 60;
		ARKitFaceTrackingConfiguration config = new ARKitFaceTrackingConfiguration();
		//config.alignment = UnityARAlignment.UnityARAlignmentGravity; // using gravity alignment enables orientation (3DOF) tracking of device camera. we don't need it
		config.alignment = UnityARAlignment.UnityARAlignmentCamera;

		config.enableLightEstimation = true;


		if (config.IsSupported) {
			
			m_session.RunWithConfig (config);

			UnityARSessionNativeInterface.ARFaceAnchorAddedEvent += FaceAdded;
			UnityARSessionNativeInterface.ARFaceAnchorUpdatedEvent += FaceUpdated;
			UnityARSessionNativeInterface.ARFaceAnchorRemovedEvent += FaceRemoved;
			//UnityARSessionNativeInterface.ARFrameUpdatedEvent += FrameUpdate; //can't get the light direction estimate to work for some reason, it freezes the app
		} else {
			Debug.Log ("ARKitFaceTrackingConfiguration not supported");
            RequireIPhoneXPanel.SetActive(true);
        }

	}

	void CatchARSessionFailed (string error) {
		//Debug.Log ("AR session failed. Error: " + error);
		ARError = error;
	}


	/* // this doesn't help at all
	IEnumerator RequestCamera() {
		yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
		if (Application.HasUserAuthorization(UserAuthorization.WebCam)) {
			Debug.Log ("Camera granted");
		} else {
			Debug.Log ("Camera denied");
		}
	}
*/




	void FaceAdded (ARFaceAnchor anchorData)
	{
		Vector3 pos = UnityARMatrixOps.GetPosition (anchorData.transform);
		Quaternion rot =  UnityARMatrixOps.GetRotation (anchorData.transform);

		if (camManager.DeviceCamUsed) {
			headCenter.transform.position = pos; // in device cam viewing mode, don't invert on x because this view is mirrored
			headCenter.transform.rotation = rot;
		} else {
			// invert on x because ARfaceAnchors are inverted on x (to mirror in display)
			headCenter.transform.position = new Vector3 (-pos.x, pos.y, pos.z); 
			headCenter.transform.rotation = new Quaternion( -rot.x, rot.y, rot.z, -rot.w); 
		}

		headCenter.SetActive (true);

		currentBlendShapes = anchorData.blendShapes;
	}

	void FaceUpdated (ARFaceAnchor anchorData)
	{
		Vector3 pos = UnityARMatrixOps.GetPosition (anchorData.transform);
		Quaternion rot =  UnityARMatrixOps.GetRotation (anchorData.transform);

		if (camManager.DeviceCamUsed) {
			headCenter.transform.position = pos; // in device cam viewing mode, don't invert on x because this view is mirrored
			headCenter.transform.rotation = rot;
		} else {
			// invert on x because ARfaceAnchors are inverted on x (to mirror in display)
			headCenter.transform.position = new Vector3 (-pos.x, pos.y, pos.z);
			headCenter.transform.rotation = new Quaternion( -rot.x, rot.y, rot.z, -rot.w);
		}

		currentBlendShapes = anchorData.blendShapes;

		if (autoEye) {

			if (currentBlendShapes.ContainsKey ("eyeBlink_L")) { // of course, eyeBlink_L refers to the RIGHT eye! (mirrored geometry)
				rightEyeClosed = currentBlendShapes ["eyeBlink_L"];
			}

			if (currentBlendShapes.ContainsKey ("eyeBlink_R")) {
				leftEyeClosed = currentBlendShapes ["eyeBlink_R"];
			}
				
			//string str = string.Format ("L={0:#.##} R={1:#.##}", leftEyeClosed, rightEyeClosed);
			//eyeInfoText = str;
		
			// these values seem to be in the 0.2 .. 0.7 range.. 
			// but sometimes, when viewing the phone low in the visual field, they get very high even while open (eyelids almost close)
			// we'll use a difference metric and if exceeded we select the most open eye

			if (Mathf.Abs (rightEyeClosed - leftEyeClosed) > 0.2f) {
				if (rightEyeClosed > leftEyeClosed) 
					openEye = OpenEye.Left;
				else 
					openEye = OpenEye.Right;
			}

			/* // old method
			if (rightEyeClosed > 0.5 && leftEyeClosed < 0.5)
				openEye = OpenEye.Left;

			if (rightEyeClosed < 0.5 && leftEyeClosed > 0.5)
				openEye = OpenEye.Right;*/
		} 

		string str;
		if (openEye == OpenEye.Left)
			str = "Left Eye";
		else 
			str = "Right Eye";

		if (autoEye)
			eyeInfoText = "Auto: " + str;
		else 
			eyeInfoText = str;
	}

	void FaceRemoved (ARFaceAnchor anchorData)
	{
		headCenter.SetActive (false);
		string str = "Lost Eye Tracking";
		eyeInfoText = str;
	}

	void FrameUpdate(UnityARCamera cam)
	{
		//can't get the light direction estimate to work for some reason, it freezes the app
		//keylight.transform.rotation = Quaternion.FromToRotation(Vector3.back, cam.lightData.arDirectonalLightEstimate.primaryLightDirection); // <- probably incorrect way to do it
		//keylight.transform.rotation = Quaternion.LookRotation(cam.lightData.arDirectonalLightEstimate.primaryLightDirection); // <- probably correct way to do it
	}


	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy()
	{
		
	}

	public void SetLeftEye() {
		autoEye = false;
		openEye = OpenEye.Left;
	}

	public void SetRightEye() {
		autoEye = false;
		openEye = OpenEye.Right;
	}

	public void SetAutoEye() {
		autoEye = true;
	}
}
