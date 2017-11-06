Shader "Custom/Grass Geometry" {
	Properties{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Cutoff("Cutoff", Range(0,1)) = 0.25
		_GrassHeight("Grass Height", Float) = 0.25
		_GrassWidth("Grass Width", Float) = 0.25
	}
		SubShader{
		Tags{ "Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout" }
		LOD 200

		Pass
	{

		CULL OFF

		CGPROGRAM
		#include "UnityCG.cginc" 
		#pragma vertex vert
		#pragma fragment frag
		#pragma geometry geom
		#pragma target 4.0

		sampler2D _MainTex;

	struct v2g
	{
		float4 pos : SV_POSITION;
		float3 norm : NORMAL;
		float4 uv : TEXCOORD0;
		float3 color : TEXCOORD1;
	};

	struct g2f
	{
		float4 pos : SV_POSITION;
		float3 norm : NORMAL;
		float2 uv : TEXCOORD0;
		float3 diffuseColor : TEXCOORD1;
	};

	half _Glossiness;
	half _Metallic;
	half _GrassHeight;
	half _GrassWidth;
	half _Cutoff;
	half _WindStength;
	half _WindSpeed;

	v2g vert(appdata_full v)
	{
		float3 v0 = v.vertex.xyz;

		v2g OUT;
		OUT.pos = v.vertex;
		OUT.norm = v.normal;
		OUT.uv = v.texcoord;
		OUT.color = tex2Dlod(_MainTex, v.texcoord).rgb;
		return OUT;
	}

	void buildQuad(inout TriangleStream<g2f> triStream, float3 points[4], float3 color)
	{
		g2f OUT;
		//float3 faceNormal = cross(points[1], points[2] - points[0]);
		float3 faceNormal = cross(points[1] - points[0], points[2] - points[0]);
		for (int i = 0; i < 4; ++i)
			{
			OUT.pos = UnityObjectToClipPos(points[i]);
			OUT.norm = faceNormal;
			OUT.diffuseColor = color;
			OUT.uv = float2(i % 2, (int)i/2);
			triStream.Append(OUT);
			}
		triStream.RestartStrip();
	}



	[maxvertexcount(24)]
	void geom(point v2g IN[1], inout TriangleStream<g2f> triStream)
	{
		float3 lightPosition = _WorldSpaceLightPos0;
		float3 normal = IN[0].norm;

		float3 perpendicularAngle = float3(0, 0, 1);
		float3 faceNormal = cross(perpendicularAngle, IN[0].norm);

		float3 v0 = IN[0].pos.xyz;
		float3 v1 = IN[0].pos.xyz + normal * _GrassHeight;

		float3 color = (IN[0].color);

		float sin30 = 0.5;
		float sin60 = 0.866f;
		float cos30 = sin60;
		float cos60 = sin30;

		g2f OUT;

		// Quad 1

		float3 quad1[4] = {v0 + perpendicularAngle * 0.5 * _GrassWidth, 
			v0 - perpendicularAngle * 0.5 * _GrassWidth,
			v1 + perpendicularAngle * 0.5 * _GrassWidth,
			v1 - perpendicularAngle * 0.5 * _GrassWidth};
		buildQuad(triStream, quad1, color);
		
		// Quad 2
		float3 quad2[4] = {v0 + float3(sin60, 0, -cos60) * 0.5 * _GrassWidth, 
			v0 - float3(sin60, 0, -cos60) * 0.5 * _GrassWidth,
			v1 + float3(sin60, 0, -cos60) * 0.5 * _GrassWidth,
			v1 - float3(sin60, 0, -cos60)* 0.5 * _GrassWidth};
		buildQuad(triStream, quad2, color);
		
		// Quad 3
		float3 quad3[4] = {v0 + float3(sin60, 0, cos60) * 0.5 * _GrassWidth, 
			v0 - float3(sin60, 0, cos60) * 0.5 * _GrassWidth,
			v1 + float3(sin60, 0, cos60) * 0.5 * _GrassWidth,
			v1 - float3(sin60, 0, cos60) * 0.5 * _GrassWidth};
		buildQuad(triStream, quad3, color);


	}

	half4 frag(g2f IN) : COLOR
	{
		fixed4 c = tex2D(_MainTex, IN.uv);
		clip(c.a - _Cutoff);
		return c;
	}
		ENDCG

	}
	}
}