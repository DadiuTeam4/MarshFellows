using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Light))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class VolumetricLightMesh : MonoBehaviour 
{
	public float MaxOpacity = 0.25f;
	public bool Octogon = false;
	private MeshFilter meshFilter;
	private Light light;

	private Mesh mesh;
	void Start () 
	{
		meshFilter = GetComponent<MeshFilter>();
		light = GetComponent<Light>();
		if(light.type != LightType.Spot)
		{
			light.type = LightType.Spot;
		}

	}
	

	void Update () 
	{
		mesh = BuildMesh();
		meshFilter.mesh = mesh;

	}

	private Mesh BuildMesh()
	{
		mesh = new Mesh();
		float octogonContant = 0.411766f;
		float farPostion = Mathf.Tan(light.spotAngle/2 * Mathf.Deg2Rad) * light.range;
		

		mesh.vertices = new Vector3[] 
		{
			new Vector3(0,0,0),
			new Vector3(-farPostion*octogonContant,farPostion,light.range),
			new Vector3(farPostion*octogonContant,farPostion,light.range),
			new Vector3(farPostion*octogonContant,farPostion,light.range),
			new Vector3(farPostion,farPostion*octogonContant,light.range),
			new Vector3(farPostion,-farPostion*octogonContant,light.range),
			new Vector3(farPostion*octogonContant,-farPostion,light.range),
			new Vector3(-farPostion*octogonContant,-farPostion,light.range),
			new Vector3(-farPostion,-farPostion*octogonContant,light.range),
			new Vector3(-farPostion,farPostion*octogonContant,light.range),



		};

		mesh.colors = new Color[] 
		{
			new Color(light.color.r, light.color.g, light.color.b, light.color.a * MaxOpacity),
			new Color(light.color.r, light.color.g, light.color.b, light.color.a * 0),
			new Color(light.color.r, light.color.g, light.color.b, light.color.a * 0),
			new Color(light.color.r, light.color.g, light.color.b, light.color.a * 0),
			new Color(light.color.r, light.color.g, light.color.b, light.color.a * 0),
			new Color(light.color.r, light.color.g, light.color.b, light.color.a * 0),
			new Color(light.color.r, light.color.g, light.color.b, light.color.a * 0),
			new Color(light.color.r, light.color.g, light.color.b, light.color.a * 0),
			new Color(light.color.r, light.color.g, light.color.b, light.color.a * 0),
			new Color(light.color.r, light.color.g, light.color.b, light.color.a * 0)
		};
		mesh.triangles = new int[]
		{
			0,1,2,
			0,2,3,
			0,3,4,
			0,4,5,
			0,5,6,
			0,6,7,
			0,7,8,
			0,8,9,
			0,9,1
		};

		return mesh;
	}
}
