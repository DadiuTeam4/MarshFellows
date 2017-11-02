//Author: Emil Villumsen
//Collaborator: Jonathan,
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

using Events;

[ExecuteInEditMode]
public class LoadAllScenes : Singleton<LoadAllScenes>
{
    void Start()
    {
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(i);
            if (!scene.isLoaded)
            {
                if (Application.isPlaying)
                {
                    SceneManager.LoadScene(i, LoadSceneMode.Additive);
                }
                else
                {
                    EditorSceneManager.OpenScene(EditorBuildSettings.scenes[i].path, OpenSceneMode.Additive);
                }
            }
        }
    }
}