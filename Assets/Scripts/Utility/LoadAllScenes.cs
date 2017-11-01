//Author: Emil Villumsen
//Collaborator: Jonathan,
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Events;

public class LoadAllScenes : Singleton<LoadAllScenes>
{

    void Start()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(i);
            if (!scene.isLoaded)
            {
                SceneManager.LoadScene(i, LoadSceneMode.Additive);
            }
        }
    }
}