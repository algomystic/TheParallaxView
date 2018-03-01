using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

// this script is for tracking the device camera. it doesn't track much at the moment since the AR session is using UnityARAlignment.UnityARAlignmentCamera, it may be used in the future 

public class CameraTracker : MonoBehaviour {

	[SerializeField]
	private Camera trackedCamera;

	private bool sessionStarted = false;

	//public CameraManager camManager;

	// Use this for initialization
	void Start () {
		UnityARSessionNativeInterface.ARFrameUpdatedEvent += FirstFrameUpdate;
	}

	void OnDestroy()
	{
	}

	void FirstFrameUpdate(UnityARCamera cam)
	{
		sessionStarted = true;
		UnityARSessionNativeInterface.ARFrameUpdatedEvent -= FirstFrameUpdate;
	}

	// Update is called once per frame
	void Update () {
		if (trackedCamera != null && sessionStarted) {
			Matrix4x4 cameraPose = UnityARSessionNativeInterface.GetARSessionNativeInterface ().GetCameraPose ();
			trackedCamera.transform.localPosition = UnityARMatrixOps.GetPosition (cameraPose);
			trackedCamera.transform.localRotation = UnityARMatrixOps.GetRotation (cameraPose);

			//Debug.Log ("device cam rotation: " + trackedCamera.transform.localRotation + " device cam position: " + trackedCamera.transform.localPosition);
			//if (camManager.EyeCamUsed) {
			//	trackedCamera.transform.localRotation = new Quaternion (0, 0, 0, -1); // don't use device rotation (landscape, portrait etc)
			//}

			trackedCamera.projectionMatrix = UnityARSessionNativeInterface.GetARSessionNativeInterface ().GetCameraProjection ();
		}
	}
}
