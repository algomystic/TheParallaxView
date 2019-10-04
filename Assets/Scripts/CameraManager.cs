using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this script is just for selecting which camera is rendering
// it's called from the UI buttons that allow the user to select camera

public class CameraManager : MonoBehaviour {

	public Camera WorldCam;
	public Camera EyeCam;
	public GameObject DeviceCam;
	public GameObject DeviceCamViz;
	public GameObject EyeCamViz;


	public bool EyeCamUsed = true;
	public bool DeviceCamUsed = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void SetWorldCam() {
		WorldCam.gameObject.SetActive(true);
		EyeCam.enabled = false;
		DeviceCam.gameObject.SetActive (false);
		EyeCamUsed = false;
		DeviceCamViz.SetActive (false);
		EyeCamViz.SetActive (true);
		DeviceCamUsed = false;
	}

	public void SeEyeCam() {
		WorldCam.gameObject.SetActive(false);
		EyeCam.enabled = true;
		DeviceCam.gameObject.SetActive(false);
		EyeCamUsed = true;
		DeviceCamViz.SetActive (false);
		EyeCamViz.SetActive (false);
		DeviceCamUsed = false;
	}

	public void SetTrackedCam() {
		WorldCam.gameObject.SetActive(false);
		EyeCam.enabled = false;
		DeviceCam.gameObject.SetActive( true );
		EyeCamUsed = false;
		DeviceCamViz.SetActive (false);
		EyeCamViz.SetActive (true);
		DeviceCamUsed = true;
	}


}
