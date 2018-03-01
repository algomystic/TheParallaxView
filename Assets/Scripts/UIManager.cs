using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script manages UI buttons and functions 

public class UIManager : MonoBehaviour {

	public GameObject SettingsPanel;
	bool settingsVisible = false;


	public GameObject BoxScene;
	public GameObject TheVoidScene;
	public Text eyeInfoText;
	public Text IPDValueText;
	public GameObject IPDSlider;
	public Text IPDLabelText;

	public HeadTrackManager headTrackManager;
	public CameraManager camManager;

	public GameObject RequireIPhoneXPanel;
	public GameObject HelpPanel;

	public void ToggleSettingsVisible () {
		
		settingsVisible = !settingsVisible;

		if (settingsVisible)
			SettingsPanel.SetActive (true);
		else
			SettingsPanel.SetActive (false);
	}

	public void ReadArticle () {
		Application.OpenURL("http://anxious-bored.com/TPV");

	}

	public void SetBoxScene() {
		BoxScene.SetActive (true);
		TheVoidScene.SetActive (false);
		RenderSettings.ambientLight = new Color (0.23f, 0.23f, 0.23f, 1.0f);
	}

	public void SetTheVoidScene() {
		BoxScene.SetActive (false);
		TheVoidScene.SetActive (true);
		RenderSettings.ambientLight = new Color (0.83f, 0.83f, 0.83f, 1.0f);
	}

	public void TryAnyway() {
		RequireIPhoneXPanel.SetActive (false);
	}


	public void DismissHelp() {
		HelpPanel.SetActive (false);
	}

	// Use this for initialization
	void Start () {

		bool deviceIsIphoneX = UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhoneX;
		if (!deviceIsIphoneX) {
			RequireIPhoneXPanel.SetActive (true);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		IPDValueText.text = string.Format ("{0} mm", (int)(headTrackManager.IPD));
		eyeInfoText.text = headTrackManager.eyeInfoText;

		if (camManager.DeviceCamUsed) {
			IPDSlider.SetActive (true);
			IPDValueText.enabled = true;
			IPDLabelText.enabled = true;
		} else {
			IPDSlider.SetActive (false);
			IPDValueText.enabled = false;
			IPDLabelText.enabled = false;
		}
	}
}
