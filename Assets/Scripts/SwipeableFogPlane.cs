﻿// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeableFogPlane : Swipeable 
{
	[Tooltip("Template used for making holes in the texture plane")]
	public Texture2D template;

	[Tooltip("How many frames there are between a fog plane texture update")]
	[Range(1, 50)]
	public int updateFramerate = 1;
	[Tooltip("How many OnSwipe calls there are between an OnSwipe call")]
	[Range(1, 100)]
	public int swipeRate = 1;
	private int currentOnSwipeCall;

	private bool updatePending = false;

	private MeshRenderer meshRenderer;
	
	private Material fogMaterial;
	private Material workingMaterial;
	
	private Texture2D fogTexture;
	private Texture2D workingTexture;

	void Start () 
	{
		currentOnSwipeCall = swipeRate;

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

	void Update()
	{
		if(updatePending && Time.frameCount % updateFramerate == 0)
		{
			workingTexture.Apply();
			updatePending = false;
		}
	}
	
	public override void OnSwipe(RaycastHit raycastHit, Vector3 direction) 
	{
		if(currentOnSwipeCall == swipeRate)
		{
			Vector2 pixelUV = raycastHit.textureCoord;
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

			updatePending = true;

			currentOnSwipeCall = 1;
		}
		else
		{
			currentOnSwipeCall ++;
		}
	}
}