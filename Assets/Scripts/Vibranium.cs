using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibranium : MonoBehaviour {

	public GameObject sphere;
	public int noSpheresX = 20;
	public int noSpheresY = 3;
	public int noSpheresZ = 3;
	public float size = 1.0f;
	public float sizeRnd = 0.5f;
	public float sizePerlin = 0.5f;
	public float ampPerlin = 0.5f;
	public float freqPerlin = 0.5f;
	public float speedPerlin = 1.0f;
	public float ampVibrate = 0.5f;
	public float speedVibrate = 0.5f;
	public float phaseVibrate = 0.1f;

	public Material mat1;
	public Material mat2;
	public Material mat3;


	GameObject [] spheres;

	// Use this for initialization
	void Start () {
		spheres = new GameObject[noSpheresX * noSpheresY * noSpheresZ];
		int i = 0;
		for (int x = 0; x < noSpheresX; x++) {
			for (int y = 0; y < noSpheresY; y++) {
				for (int z = 0; z < noSpheresZ; z++) {
					spheres [i] = Instantiate (sphere, this.transform);
					spheres [i].transform.localPosition = new Vector3 (x, y, z);
					spheres [i].transform.localScale = new Vector3 (size, size, size);


					if (z<2) 
						spheres [i].GetComponent<Renderer> ().material = mat3;
					else if (z<5) 
						spheres [i].GetComponent<Renderer> ().material = mat2;
					else 
						spheres [i].GetComponent<Renderer> ().material = mat1;

					i++;
				}
			}
		}		
	}
	
	// Update is called once per frame
	void Update () {
		
		int i = 0;

		for (int x = 0; x < noSpheresX; x++) {
			for (int y = 0; y < noSpheresY; y++) {
				for (int z = 0; z < noSpheresZ; z++) {

					Vector3 pos = new Vector3 (x, y, z);

					pos.x += ampPerlin * Perlin.Noise (x*freqPerlin + speedPerlin*Time.time, y*freqPerlin, z*freqPerlin);
					pos.y += ampPerlin * Perlin.Noise (x*freqPerlin + speedPerlin*Time.time, y*freqPerlin, z*freqPerlin + 13.2f);
					pos.z += ampPerlin * Perlin.Noise (x*freqPerlin + speedPerlin*Time.time, y*freqPerlin, z*freqPerlin + 49.9f);

					float scaleP = 1f + sizePerlin * Perlin.Noise (x*freqPerlin + speedPerlin*Time.time, y*freqPerlin, z*freqPerlin + 121.3f);

					pos.x += (0.9f*scaleP+0.1f)*ampVibrate*(Mathf.Sin( speedVibrate * Time.time + phaseVibrate*i));
					pos.y += (0.9f*scaleP+0.1f)*ampVibrate*(Mathf.Cos( speedVibrate * Time.time + phaseVibrate*i));
					pos.z += (0.9f*scaleP+0.1f)*ampVibrate*(Mathf.Cos( speedVibrate * Time.time + 0.5f*Mathf.PI + phaseVibrate*i));

					spheres [i].transform.localPosition = pos;

					Vector3 scale = new Vector3 (size, size, size);
					scale *= scaleP;

					spheres [i].transform.localScale = scale;

					i++;
				}
			}
		}		
	}
}
