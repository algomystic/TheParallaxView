Shader "Custom/BoxBlurH"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		//_Near ("Near", Float) = 0.2
		//_Range ("Range", Float) = 20.0
		_Spread("Spread", Float) = 5.0
		_BlurMult("BlurMult", Float) = 5.0
		//_HalfBlurH("HalfBlurH", Int) = 2
		//_HalfBlurV("HalfBlurV", Int) = 2
		_DivBlur("DivBlur", Float) = 1.0
		_Screen2Tex("Screen2Tex", Vector) = (1.0,1.0,0.0,0.0)

	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
//			sampler2D _CameraDepthTexture;

			//float _Near;
			//float _Range;
			float _Spread;
			float _BlurMult;
			float4 _Screen2Tex;
			//int _HalfBlurH;
			//int _HalfBlurV;
			float _DivBlur;

			fixed4 frag (v2f i) : SV_Target
			{
				float4 col = tex2D(_MainTex, i.uv);
			
				float4 blur_sum = 0;

				//for (int x = -_HalfBlurH; x<=_HalfBlurH; x++) {
				for (int x = -5; x<=5; x++) {
					//for (int y = -_HalfBlurV; y<=_HalfBlurV; y++) {
						float2 uv = i.uv + _Spread*_Screen2Tex.xy*float2(x,0);

						float4 c = tex2D(_MainTex, uv );

						if (c.r==1.0) c = col;

						float d = (c.r*_BlurMult-1.0) * _Spread;
						//float l = x;

						float cd = clamp( (d-x), 0.0, 1.0);
						float cl = 1.0-cd;

						blur_sum += cd*c + cl*col;
					//}
				}
				col = blur_sum*_DivBlur;
				return col;
			}

			ENDCG
		}
	}
}
