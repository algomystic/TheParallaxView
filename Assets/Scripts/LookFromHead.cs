using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookFromHead : MonoBehaviour {

	public Transform head;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.rotation = Quaternion.LookRotation (-head.position);	
	}
}
