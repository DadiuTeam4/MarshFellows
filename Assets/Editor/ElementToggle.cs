// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ElementToggle : EditorWindow 
{
	private GameObject fogCurtain;
	private Canvas[] uiElements;
	private bool initialized;

	[MenuItem("Window/Element Toggling")]
	static void Setup() 
	{
		ElementToggle window = (ElementToggle) EditorWindow.GetWindow(typeof(ElementToggle), false, "Element Toggle");
		window.minSize = new Vector2(10, 10);
        
        window.Show();
	}

	void OnGUI()
	{
		if (!initialized)
		{
			fogCurtain = GameObject.Find("StartGameFogCurtain");
			uiElements = FindObjectsOfType<Canvas>();
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
		if (GUILayout.Button("Toggle start fog curtain", button))
		{
			if (fogCurtain)
			{
				fogCurtain.SetActive(!fogCurtain.activeInHierarchy);
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

		if (GUILayout.Button("Toggle all UI elements"))
		{
			if (uiElements != null && uiElements.Length != 0)
			{
				foreach (Canvas canvas in uiElements)
				{
					canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy);
				}
			}
			else 
			{
				Debug.Log("Finding UI elements...");
				uiElements = FindObjectsOfType<Canvas>();
				if (uiElements == null)
				{
					Debug.LogError("Could not find UI elements");
				}
			}
		}
		GUILayout.EndVertical();
	}
}
