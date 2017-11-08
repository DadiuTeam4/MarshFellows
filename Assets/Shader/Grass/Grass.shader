Shader "Custom/GrassTerrainShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
		_WindSpeed("Wind Speed", Range(0,1)) = 0.5
		_WindStrength("Wind Strength", Range(0,1)) = 0.5
		_WindDir("Wind Direction", Vector) = (0,0,0,0)

	}
	SubShader {
		Tags { "Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout" }
		LOD 200
		Cull Off
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows addshadow alphatest:_Cutoff vertex:vert
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};
		
		float _WindSpeed;
		float4 _WindDir;
		float1 _WindStrength;

		float4x4 mWorldViewProjMatrix;
		float4 vLight;
		float fObjectHeight;
		half _Glossiness;
		half _Metallic;
		//fixed4 _Color;



		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
		UNITY_INSTANCING_CBUFFER_END

		void vert(inout appdata_full v)
		{
			_WindDir = normalize(_WindDir);
			float3 center = float3(0,-0.5,0);
			float lengthFromCenter = distance(v.vertex, center);

			v.vertex.xz += sin(_Time.w * _WindSpeed) * _WindStrength * (v.vertex.y + 0.5) * _WindDir.xz;
			v.vertex.y += (sin(_Time.w * _WindSpeed)  * _WindStrength * 0.1 + 1) * (v.vertex.y + 0.5);
			

		}


		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex - float2(0, 0.05)) * UNITY_ACCESS_INSTANCED_PROP(_Color);
			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
