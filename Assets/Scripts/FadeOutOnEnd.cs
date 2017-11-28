//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class FadeOutOnEnd : MonoBehaviour {

     public Texture2D tex;
     private Texture2D texCopy;
     
     public Material fogMaterial;
     private bool fade;
     private float alph;

    EventManager eventManager;

     void Start(){
         //get a fog texture from the material
        texCopy = fogMaterial.mainTexture as Texture2D;
        Debug.Log(texCopy.GetType() + "   "+ texCopy.format);
       
        tex = new Texture2D(texCopy.width, texCopy.height, TextureFormat.RGBA32, texCopy.mipmapCount > 1);
        
        byte[] pix = tex.GetRawTextureData();

        tex.LoadRawTextureData(pix);

        tex.SetPixel (0, 0, new Color(0,0,0,0));
        tex.Apply ();

		eventManager = EventManager.GetInstance();

    	EventDelegate flipper = FlipFadeForListener;

		eventManager.AddListener(CustomEvent.DeerKilledEvent, flipper);
     }
     // put it on your screen
     void OnGUI(){
         GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height),tex);
     }
     
     void Update () {//play around with oppacity
         if(Input.GetKeyDown("space"))
		 {
			 FlipFade();
		 }
         
         
         if (!fade) {
             if (alph > 0) {
                 alph -= Time.deltaTime * .2f;
                 if (alph < 0) {alph = 0f;}
                 tex.SetPixel (0, 0, new Color (0, 0, 0, alph));
                 tex.Apply ();
             }
         } 
         if (fade) {
             if (alph < 1) {
                 alph += Time.deltaTime * .2f;
                 if (alph > 1) {alph = 1f;}
                 tex.SetPixel (0, 0, new Color (0, 0, 0, alph));
                 tex.Apply ();
             }
         }
     }
	void FlipFadeForListener(EventArgument argument)
	{
		FlipFade();
	}

	void FlipFade()
	{
		fade=!fade;
	}
    
}
