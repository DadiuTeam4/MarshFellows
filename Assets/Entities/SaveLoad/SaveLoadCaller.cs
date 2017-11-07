//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadCaller : MonoBehaviour {

    void OnApplicationQuit()
    {
		SaveLoadManager.Save();
        Debug.Log("Application ending after " + Time.time + " seconds");
    }

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
		SaveLoadManager.Load();
        Debug.Log("Before scene loaded");
    }
}
