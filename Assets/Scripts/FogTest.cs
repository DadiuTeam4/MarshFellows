// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTest : Holdable 
{
	public float radius = 10f;
	public Texture2D template;

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
		int x = (int) (pixelUV.x * workingTexture.width);
		int y = (int) (pixelUV.y * workingTexture.height);

		x -= template.width / 2;
		y -= template.height /2;

		float alpha;

		for(int i = 0; i < template.width; i++)
		{
			for(int j = 0; j < template.height; j++)
			{
				alpha = template.GetPixel(i,j).r;
				Color col = workingTexture.GetPixel(i + x, j + y);
				col.a = col.a - alpha;

				workingTexture.SetPixel(i + x, j + y, col);
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
