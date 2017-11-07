// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTest : Holdable 
{
	public float radius = 10f;

	private MeshRenderer meshRenderer;
	private Material fogMaterial;
	private Material workingMaterial;
	private Texture2D fogTexture;
	private Texture2D workingTexture;

	void Start () 
	{
		meshRenderer = GetComponent<MeshRenderer>();
		fogMaterial = meshRenderer.material;
		fogTexture = fogMaterial.GetTexture("_MainTex") as Texture2D;
	
		workingMaterial = Material.Instantiate(fogMaterial);

	 	workingTexture = new Texture2D (fogTexture.width, fogTexture.height, TextureFormat.ARGB32, false);
 		workingTexture.SetPixels32(fogTexture.GetPixels32());
 		workingTexture.Apply();

		meshRenderer.material = workingMaterial;
		workingMaterial.SetTexture("_MainTex", workingTexture);
	}	
	
	public override void OnTouchBegin(RaycastHit hit) 
	{
		Vector2 pixelUV = hit.textureCoord;
		pixelUV.x *= workingTexture.width;
		pixelUV.y *= workingTexture.height;

		for (int i = 0; i < radius; i++)
		{
			for(int j = 0; j < radius; j++)
			{
				Color col = workingTexture.GetPixel(i + (int)pixelUV.x, j + (int)pixelUV.y);
				col.a = 0;
				workingTexture.SetPixel(i + (int)pixelUV.x, j + (int)pixelUV.y, col);
			}
		}

		workingTexture.Apply();
	}
	
	public override void OnTouchHold(RaycastHit hit) 
	{

	}
	
	public override void OnTouchReleased() 
	{

	}
}
