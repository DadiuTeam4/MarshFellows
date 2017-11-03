using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ImageEffectAllowedInSceneView]
[ExecuteInEditMode]

public class FogImageEffect : MonoBehaviour {

	private Camera camera;
	public Material effectMaterial;

	// Use this for initialization
	void Start () {

		/* This was generating errors all the time, so I commented it out.
		Please put "//Author: <Your name> //Contributors: " in the top of your Unity 
		script template so your scripts always have your name in the top.*/

		// camera.GetComponent<Camera>();
		// camera.depthTextureMode = DepthTextureMode.Depth;
	}
[ImageEffectOpaque]	
	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		Graphics.Blit(src, dst, effectMaterial);
	}
}
