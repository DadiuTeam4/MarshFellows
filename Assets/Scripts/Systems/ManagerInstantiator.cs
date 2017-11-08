//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ManagerInstantiator : MonoBehaviour
{
    public GameObject managerPrefab;
    public GameObject managerInstance;

    void OnEnable()
    {
        if (!managerInstance && !FindObjectOfType<DontDestroyOnLoad>())
        {
            managerInstance = Instantiate(managerPrefab);
        }
    }
}
