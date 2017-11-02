//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ManagerInstantiator : MonoBehaviour
{
    public GameObject managerPrefab;

    void Awake()
    {
        if (GameObject.FindWithTag("ManagerParent") == null)
        {
            Instantiate(managerPrefab);
            //Instantiate(managerPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
