﻿Shader "Custom/Cel Water" 
{
	Properties 
	{
		_Color 			("Color", Color) = (1,1,1,1)
		_MainTex 		("Albedo (RGB)", 2D) = "white" {}
		_UnlitSurface 	("UnlitSurface", Range(0,1)) = 0.75 
		_Stepping 		("Stepping", Range(0,1)) = 0.0025
		_LightType 		("Ligth Type", Range(0,1)) = 1
		_ShadowColor 	("ShadowColor", Color) = (0.10,0.5,1,1)
		_BumpMap		("Normal Map", 2D) = "bump" {}
		_Speed 			("Water Speed", Vector) = (0,0,0,0)
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf CelShadingForward 
		#pragma target 3.0
		#pragma multi_compile_fog

		sampler2D _MainTex;
		sampler2D _BumpMap;

		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};

		half _Stepping;
		half _LightType;
		fixed4 _Color;
		fixed _UnlitSurface;
		fixed4 _ShadowColor;
		fixed4 _Speed;
		
		half4 LightingCelShadingForward(SurfaceOutput s, half3 lightDir, half atten) 
		{
			half NdotL = dot(s.Normal, lightDir);
	 		half NdotL0 = clamp(floor(NdotL), _UnlitSurface, 1);
			half NdotL1 = smoothstep(0, _Stepping, NdotL) * _UnlitSurface; 
			NdotL = lerp(NdotL0, NdotL1, _LightType);
			
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

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex + _Speed.xy * _Time.xx) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap + _Speed.xy * _Time.xx));
		}
		ENDCG
	}
	FallBack "Diffuse"
}
