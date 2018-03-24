using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostBlur : MonoBehaviour {

	//public Material mat;


	public Material matH;
	public Material matV;

	public Camera cam;
	public OffAxisProjection proj;
	bool isActive = false;

	[Space(20)]

	public float pass1_BlurMult = 10f;
	public float pass1_Spread = 5f;

	[Space(20)]

	public float pass2_BlurMult = 7f;
	public float pass2_Spread = 4f;

	[Space(20)]

	public float pass3_BlurMult = 4f;
	public float pass3_Spread = 3f;

	[Space(20)]

	public float pass4_BlurMult = 3f;
	public float pass4_Spread = 1f;


	// Use this for initialization
	void Start () {
		//cam.depthTextureMode = DepthTextureMode.Depth;
	}
	
	// Update is called once per frame
	void Update () {
		//mat.SetFloat ("_Near", proj.nearDist);
	}


	public void Activate() {
		//cam.depthTextureMode = DepthTextureMode.Depth;
		isActive = true;
	}

	public void DeActivate() {
		//cam.depthTextureMode = DepthTextureMode.None;
		isActive = false;
	}


	void OnRenderImage(RenderTexture src, RenderTexture dst) {
		if (isActive) {

			//mat.SetVector ("_Screen2Tex", new Vector4( 1f / Screen.width, 1f/Screen.height, 0f, 0f ));
			matH.SetVector ("_Screen2Tex", new Vector4( 1f / Screen.width, 1f/Screen.height, 0f, 0f ));
			matV.SetVector ("_Screen2Tex", new Vector4( 1f / Screen.width, 1f/Screen.height, 0f, 0f ));
			matH.SetFloat ("_DivBlur", 1f / 11f);
			matV.SetFloat ("_DivBlur", 1f / 11f);

			//Graphics.Blit (src, dst, mat);

			RenderTexture tmp = RenderTexture.GetTemporary (Screen.width, Screen.height);

			matH.SetFloat ("_Spread", pass1_Spread);
			matH.SetFloat ("_BlurMult", pass1_BlurMult);
			//matH.SetInt ("_HalfBlurH", pass1_HalfBlurH);
			//matH.SetInt ("_HalfBlurV", pass1_HalfBlurV);
			//matH.SetFloat ("_DivBlur", 1f / ((1f + 2f * pass1_HalfBlurH) * (1f + 2f * pass1_HalfBlurV)));
			//matH.SetFloat ("_DivBlur", 1f / 11f);

			Graphics.Blit (src, tmp, matH);

			matV.SetFloat ("_Spread", pass2_Spread);
			matV.SetFloat ("_BlurMult", pass2_BlurMult);
			//matV.SetInt ("_HalfBlurH", pass2_HalfBlurH);
			//matV.SetInt ("_HalfBlurV", pass2_HalfBlurV);
			//matV.SetFloat ("_DivBlur", 1f / ((1f + 2f * pass2_HalfBlurH) * (1f + 2f * pass2_HalfBlurV)));


			Graphics.Blit (tmp, src, matV);

			matH.SetFloat ("_Spread", pass3_Spread);
			matH.SetFloat ("_BlurMult", pass3_BlurMult);
			//matH.SetInt ("_HalfBlurH", pass3_HalfBlurH);
			//matH.SetInt ("_HalfBlurV", pass3_HalfBlurV);
			//matH.SetFloat ("_DivBlur", 1f / ((1f + 2f * pass3_HalfBlurH) * (1f + 2f * pass3_HalfBlurV)));


			Graphics.Blit (src, tmp, matH);

			matV.SetFloat ("_Spread", pass4_Spread);
			matV.SetFloat ("_BlurMult", pass4_BlurMult);
			//matV.SetInt ("_HalfBlurH", pass4_HalfBlurH);
			//matV.SetInt ("_HalfBlurV", pass4_HalfBlurV);
			//matV.SetFloat ("_DivBlur", 1f / ((1f + 2f * pass4_HalfBlurH) * (1f + 2f * pass4_HalfBlurV)));


			Graphics.Blit (tmp, dst, matV);

			RenderTexture.ReleaseTemporary(tmp);
		}
	}
}
