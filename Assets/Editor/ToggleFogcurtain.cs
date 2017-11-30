// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToggleFogcurtain : EditorWindow 
{
	private GameObject fogCurtain;
	private bool initialized;

	[MenuItem("Window/Fog Curtain Toggle Button")]
	static void Setup() 
	{
		ToggleFogcurtain window = (ToggleFogcurtain) EditorWindow.GetWindow(typeof(ToggleFogcurtain), false, "Fog toggle");
		window.minSize = new Vector2(10, 10);
        
        window.Show();
	}

	void OnGUI()
	{
		if (!initialized)
		{
			fogCurtain = GameObject.Find("StartGameFogCurtain");
			initialized = true;
		}

		GUIStyle background             = new GUIStyle("box");
        background.margin               = new RectOffset();
        background.normal.background    = Resources.Load("background", typeof(Texture2D)) as Texture2D;
        background.stretchWidth         = true;

		GUIStyle button                 = new GUIStyle("button");
        button.margin                   = new RectOffset(3, 0, 0, 0);
        //button.stretchWidth             = true;
        //button.alignment                = TextAnchor.MiddleLeft;
        button.fixedHeight              = 20;

		GUILayout.BeginVertical(background);
		if (GUILayout.Button("Toggle fog curtain", button))
		{
			if (fogCurtain)
			{
				fogCurtain.SetActive(!fogCurtain.active);
			}
			else
			{
				Debug.Log("Finding fog curtain...");
				fogCurtain = GameObject.Find("StartGameFogCurtain");
				if (!fogCurtain)
				{
					Debug.LogError("Could not find fog curtain. Check that the name is set to \"StartGameFogCurtain\"");
				}
			}
		}
		GUILayout.EndVertical();
	}
}
