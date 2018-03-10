using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SceneInfo script. Attach to each scene so the scene manager knows how to handle it

public class SceneInfo : MonoBehaviour {

	public string SceneName;
	public bool use;
	public Color ambientLight;
	public bool headLight; // a light that moves with user's head, good for specular

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
