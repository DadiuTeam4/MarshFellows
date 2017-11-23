//Author Troels
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class IkHandlerGeneric : MonoBehaviour
{

    Animator anim;

    public Vector3 Offset;

    public Transform lookPos;

    public Transform head;
    public Transform neckA;
    public Transform neckB;
    public Transform chest;

    [Range(0, 1)] public float headWeight = 1;
    [Range(0, 1)] public float neckWeight = 1;
    [Range(0, 1)] public float chestWeight = 1;


    Vector3 currentPosDir;

    // Use this for initialization
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 CalulateWeightedLook(Transform bone, float weight)
    {
        float distance = Vector3.Distance(bone.position, lookPos.position);
        Vector3 forwardPoint = bone.position + Quaternion.Euler(new Vector3(0, -90, 0)) * transform.forward * distance;
        Vector3 result = forwardPoint + (lookPos.position - forwardPoint) * weight;
        return result;
    }

    private void LateUpdate()
    {
        //Transform bone = head;
        //float distance = Vector3.Distance(bone.position, lookPos.position);
        //Vector3 forwardPoint = bone.position + Quaternion.Euler(new Vector3(0, -90, 0)) * transform.forward * distance;
        //Vector3 result = forwardPoint + (lookPos.position - forwardPoint) * headWeight;

        chest.LookAt(CalulateWeightedLook(chest, chestWeight));
        chest.rotation = chest.rotation * Quaternion.Euler(Offset);

        neckA.LookAt(CalulateWeightedLook(neckA, neckWeight));
        neckA.rotation = neckA.rotation * Quaternion.Euler(Offset);

        head.LookAt(CalulateWeightedLook(head, headWeight));
        head.rotation = head.rotation * Quaternion.Euler(Offset);

    }

    private void OnDrawGizmos()
    {
        Vector3 currentPosDir = Quaternion.Euler(new Vector3(0, -90, 0)) * head.forward;

        float distance = Vector3.Distance(head.position, lookPos.position);
        Vector3 forwardPoint = head.position + Quaternion.Euler(new Vector3(0, -90, 0)) * transform.forward*distance;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(head.position, head.position + (lookPos.position - head.position));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(head.position, forwardPoint);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(forwardPoint, forwardPoint + (lookPos.position - forwardPoint) * headWeight);
        
    }

}