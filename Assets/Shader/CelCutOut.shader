Shader "Custom/Cel Cut Out" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_UnlitSurface ("UnlitSurface", Range(0,1)) = 0.0 
		_ShadowColor ("ShadowColor", Color) = (0.10,0.5,1,1)
		 _BumpMap("Normal Map", 2D) = "bump" {}
	}
	SubShader {
		Tags { "Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout" }
		LOD 200
		Cull Off
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		//#pragma surface surf Standard fullforwardshadows
		#pragma surface surf CelShadingForward alphatest:_Cutoff
		#pragma target 3.0
		#pragma multi_compile_fog

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed _UnlitSurface;
		fixed4 _ShadowColor;
		sampler2D _BumpMap;

		half4 LightingCelShadingForward(SurfaceOutput s, half3 lightDir, half atten) {
			
			_UnlitSurface = _UnlitSurface -1;
			half NdotL = dot(s.Normal, lightDir);
	 		//NdotL = 1 + clamp(floor(NdotL), _UnlitSurface, 0);
			NdotL = smoothstep(0, 0.025f, NdotL); 
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten) + (1-atten) * _ShadowColor;
			c.a = s.Alpha;
			return c;
		}


		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
		}
		ENDCG
	}
	FallBack "Diffuse"
}
