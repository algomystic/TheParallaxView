using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// little scene manager to report the scene names to the UI, and set active scene

public class SceneManager : MonoBehaviour {

	List<GameObject> Scenes;

	// Use this for initialization
	void Awake () { // Awake is called before Start, so we know this has been done when UIManager calls us from its Start()
		Scenes = new List<GameObject>();

		foreach(Transform child in transform)
		{
			SceneInfo si = child.GetComponent<SceneInfo> ();
			if (si.use)
				Scenes.Add (child.gameObject);
			else
				child.gameObject.SetActive (false); // make sure unused scenes are off
		}
	}

	public int GetNoScenes() {
		return Scenes.Count;
	}

	public string GetSceneName(int SceneNo) {
		return Scenes [SceneNo].GetComponent<SceneInfo> ().SceneName;
	}

	public void SetActiveScene(int SceneNo) {
		for (int i = 0; i < Scenes.Count; i++) {
			SceneInfo si = Scenes [i].GetComponent<SceneInfo> ();
			if (i == SceneNo) {
				Scenes [i].SetActive (true);
				RenderSettings.ambientLight = si.ambientLight;
			} else {
				Scenes [i].SetActive (false);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
