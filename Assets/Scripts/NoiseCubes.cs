using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseCubes : MonoBehaviour {

	public GameObject cube;

	int nx = 20;
	int ny = 10;

	GameObject [] boxes;
	Renderer [] renderers;

	public float sizeX;
	public float sizeY;
	public float fscale;
	public float fspeed;
	public float wave_xfreq;
	public float wave_yfreq;
	public float wave_zamp;
	public float wave_pscale;
	public float wave_xspeed;
	public float wave_yspeed;
	public float AO;

	// Use this for initialization
	void Start () {

		boxes = new GameObject[nx*ny];
		renderers = new Renderer[nx*ny];

		for (int x = 0; x < nx; x++) {
			for (int y = 0; y < ny; y++) {
				Vector3 pos = new Vector3 (x * sizeX, y * sizeY, 2f);
				//Quaternion rot = Quaternion.identity;
				//boxes [x + y*nx] = Instantiate (cube, pos, rot, this.transform);
				boxes [x + y * nx] = Instantiate (cube, this.transform);
				boxes [x + y * nx].transform.localPosition = pos;


				Vector3 scale = new Vector3 (1f, 1f, 5f); 
				boxes [x + y * nx].transform.localScale = scale;

				Renderer rend = boxes [x + y*nx].GetComponent<Renderer>();
				rend.material = new Material(Shader.Find("Standard"));
				rend.material.color = Color.white;
				rend.material.SetFloat ("_Glossiness", 0f);

				renderers [x + y * nx] = rend;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int x = 0; x < nx; x++) {
			for (int y = 0; y < ny; y++) {
				Vector2 c = new Vector2 (2f * ((float)x / (nx - 1) - 0.5f), 2f * ((float)y / (ny - 1) - 0.5f));
				float intens = 1f - c.magnitude;
				if (intens <= 0f)
					continue;
					//intens = 0f;
				
				Vector3 pos = new Vector3 (x * sizeX, y * sizeY, 0f);
				pos.z = 2f+intens * wave_zamp * (1f + 0.5f*Mathf.Sin (x * wave_xfreq + Time.time * wave_xspeed) + 0.5f*Mathf.Sin(y * wave_yfreq + Time.time * wave_yspeed) );
				//float s = 1f + wave_pscale * Mathf.Abs (pos.z);



				boxes [x + y * nx].transform.localPosition = pos;
				//boxes [x + y * nx].transform.localRotation = rot;
				//boxes [x + y * nx].transform.localScale = scale;

				//Renderer rend = boxes [x + y*nx].GetComponent<Renderer>();
				float col = 1f - pos.z * AO;
				renderers [x + y * nx].material.color = new Color (col, col, col);

			}
		}
	}
}
