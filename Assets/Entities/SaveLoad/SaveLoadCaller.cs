//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class SaveLoadCaller : MonoBehaviour {

    void OnApplicationQuit()
    {
		SaveLoadManager.Save();
    }

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
		SaveLoadManager.Load();
    }


}
