//Author Troels
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkHandlerGeneric : MonoBehaviour
{

    Animator anim;

    public Vector3 Offset;

    private Transform lookPos;

    public string tagToLookAt = "O";

    public Transform head;
    public Transform neckA;
    public Transform chest;

    [Range(0, 1)] public float headWeight = 1;
    [Range(0, 1)] public float neckWeight = 0.66f;
    [Range(0, 1)] public float chestWeight = 0.33f;

    private bool lookAtTarget = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        GameObject target = GameObject.FindGameObjectWithTag(tagToLookAt);
        if (target)
        {
            lookPos = target.transform;
        }
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
        if (lookAtTarget && lookPos)
        {
            chest.LookAt(CalulateWeightedLook(chest, chestWeight));
            chest.rotation *= Quaternion.Euler(0, 90, 0);
            neckA.LookAt(CalulateWeightedLook(neckA, neckWeight));
            neckA.rotation *= Quaternion.Euler(0, 90, 0);
            head.LookAt(CalulateWeightedLook(head, headWeight));
            head.rotation *= Quaternion.Euler(0, 90, 0);
        }

    }

    public void LookForward()
    {
        lookAtTarget = false;
    }

    public void LookAtHunters()
    {
        lookAtTarget = true;
    }

    /* private void OnDrawGizmos()
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
        
    } */

}