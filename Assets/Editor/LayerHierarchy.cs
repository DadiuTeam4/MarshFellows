// Author: Mathias Dam Hedelund
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System.Reflection;

public class LayerHierarchy : EditorWindow 
{
	private static readonly int numLayers = 31;
	private Vector2 scrollPos;
	private bool[] showLayer;
	private bool initialized = false;

	[MenuItem("Window/Layer Hierarchy")]
	static void Setup()
    {
        LayerHierarchy window = (LayerHierarchy) EditorWindow.GetWindow(typeof(LayerHierarchy), false, "Layer Hierarchy");
        window.minSize = new Vector2(10, 10);
        
        window.Show();
    }

	private GameObject[] GetAllGameobjectsInLayer(GameObject[] gameObjects, int layer)
	{
		List<GameObject> gameObjectsInLayer = new List<GameObject>();
		foreach (GameObject gameObject in gameObjects)
		{
			if (gameObject.layer == layer)
			{
				gameObjectsInLayer.Add(gameObject);
			}
		}
		if (gameObjectsInLayer.Count == 0)
		{
			return null;
		}
		return gameObjectsInLayer.ToArray();
	}
	
	private void OnGUI() 
	{
		if (!initialized)
		{
			showLayer = new bool[numLayers];
			initialized = true;
		}
		// Styles
		GUIStyle background             = new GUIStyle("box");
        background.margin               = new RectOffset();
        background.normal.background    = Resources.Load("background", typeof(Texture2D)) as Texture2D;
        background.stretchWidth         = true;

		GUIStyle button                 = new GUIStyle("label");
        button.margin                   = new RectOffset(3, 0, 0, 0);
        //button.stretchWidth             = true;
        //button.alignment                = TextAnchor.MiddleLeft;
        button.fixedHeight              = 20;

		GUIStyle label					= new GUIStyle("label");
		button.margin                   = new RectOffset(3, 0, 0, 0);
        button.stretchWidth             = true;
        button.alignment                = TextAnchor.MiddleLeft;
        button.fixedHeight              = 20;


		GUILayout.BeginVertical(background);
		EditorGUI.indentLevel = 0;
		GUILayout.Label("All GameObjects by layer:", label);
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

		GameObject[] gameObjects = FindObjectsOfType<GameObject>();
		for (int i = 0; i < numLayers; i++)
		{
			if (LayerMask.LayerToName(i) != "")
			{
				showLayer[i] = EditorGUILayout.Foldout(showLayer[i], LayerMask.LayerToName(i));
				EditorGUI.indentLevel = 1;
				if (showLayer[i])
				{
					GameObject[] gameObjectsInLayer = GetAllGameobjectsInLayer(gameObjects, i);
					if (gameObjectsInLayer != null)
					{
						foreach (GameObject gameObject in gameObjectsInLayer)
						{
							GUILayout.BeginHorizontal(background);
							if (GUILayout.Button(gameObject.name, button))
							{
								EditorGUIUtility.PingObject(gameObject);
							}
							GUILayout.EndHorizontal();
							GUILayout.Space(-10);
						}
					}
				}
				// GUILayout.Space(5);
				EditorGUI.indentLevel = 0;
			}
		}
		EditorGUILayout.EndScrollView();
		GUILayout.EndVertical();
	}
}
