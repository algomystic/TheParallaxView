using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script manages UI buttons and functions 

public class UIManager : MonoBehaviour {

	public GameObject SettingsPanel;
	bool settingsVisible = false;
	public SceneManager sceneManager;

	// scenes
	//public GameObject BoxScene;
	//public GameObject TheVoidScene;
	//public GameObject BeepleScene;

	public Text eyeInfoText;
	public Text IPDValueText;
	public GameObject IPDSlider;
	public Text IPDLabelText;

	public HeadTrackManager headTrackManager;
	public CameraManager camManager;

	public GameObject RequireIPhoneXPanel;
	public GameObject HelpPanel;

	// ugly but I don't wanna figure out how to dynamically create UI buttons right now TODO
	public Button Scene0; // box scene
	public Button Scene1; // void
	public Button Scene2; // beeple
	public Button Scene3;
	public Button Scene4;

	int activeScene;
	public GameObject theVoidBrightnessSlider;
	public GameObject theVoidBrightnessLabel; 

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

	// ugly but I don't wanna figure out how to dynamically create UI buttons right now TODO
	public void SetScene0 () {
		sceneManager.SetActiveScene (0);
		activeScene = 0;
	}

	public void SetScene1 () {
		sceneManager.SetActiveScene (1);
		activeScene = 1;
	}

	public void SetScene2 () {
		sceneManager.SetActiveScene (2);
		activeScene = 2;
	}

	public void SetScene3 () {
		sceneManager.SetActiveScene (3);
		activeScene = 3;
	}

	public void SetScene4 () {
		sceneManager.SetActiveScene (4);
		activeScene = 4;
	}

	/*
	public void SetBoxScene() {
		BoxScene.SetActive (true);
		TheVoidScene.SetActive (false);
		BeepleScene.SetActive (false);
		RenderSettings.ambientLight = new Color (0.23f, 0.23f, 0.23f, 1.0f);
	}

	public void SetTheVoidScene() {
		BoxScene.SetActive (false);
		TheVoidScene.SetActive (true);
		BeepleScene.SetActive (false);
		RenderSettings.ambientLight = new Color (0.83f, 0.83f, 0.83f, 1.0f);
	}

	public void SetBeepleScene() {
		BoxScene.SetActive (false);
		TheVoidScene.SetActive (false);
		BeepleScene.SetActive (true);
		RenderSettings.ambientLight = new Color (0.23f, 0.23f, 0.23f, 1.0f);
	}*/


	public void TryAnyway() {
		RequireIPhoneXPanel.SetActive (false);
	}


	public void DismissHelp() {
		HelpPanel.SetActive (false);
	}

	public void DisableSleep( bool disable_sleep ) {
		if (disable_sleep) {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		} else {
			Screen.sleepTimeout = SleepTimeout.SystemSetting;
		}
	}

	// Use this for initialization
	void Start () {

		bool deviceIsIphoneX = UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhoneX;
		if (!deviceIsIphoneX) {
			RequireIPhoneXPanel.SetActive (true);
		}

		// ugly but I don't wanna figure out how to dynamically create UI buttons right now TODO
		Button[] SceneButtons = new Button[5];
		SceneButtons [0] = Scene0;
		SceneButtons [1] = Scene1;
		SceneButtons [2] = Scene2;
		SceneButtons [3] = Scene3;
		SceneButtons [4] = Scene4;

		// setup scene buttons
		int NoScenes = sceneManager.GetNoScenes();
		for (int i = 0; i < 5; i++) {
			if (i < NoScenes) {
				SceneButtons [i].GetComponentInChildren<Text> ().text = sceneManager.GetSceneName (i);
			} else {
				SceneButtons [i].gameObject.SetActive (false);
			}
		}
	}


	
	// Update is called once per frame
	void Update () {

		if (settingsVisible) {

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

			if (activeScene == 1) { // the void
				theVoidBrightnessLabel.SetActive (true);
				theVoidBrightnessSlider.SetActive (true);
			} else {
				theVoidBrightnessLabel.SetActive( false );
				theVoidBrightnessSlider.SetActive (false);
			}
		}
	}
}
