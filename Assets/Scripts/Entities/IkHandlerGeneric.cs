//Author Troels
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkHandlerGeneric : MonoBehaviour
{

    Animator anim;

    public Vector3 Offset;

    public Transform lookPos;

    public Transform head;
    public Transform neck;
    public Transform chest;

    public float headWeight = 1;

    // Use this for initialization
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        head.LookAt(lookPos.position);
        head.rotation = head.rotation * Quaternion.Euler(Offset);

        neck.LookAt(lookPos.position);
        neck.rotation = neck.rotation * Quaternion.Euler(Offset);

        chest.LookAt(lookPos.position);
        chest.rotation = chest.rotation * Quaternion.Euler(Offset);
    }


}