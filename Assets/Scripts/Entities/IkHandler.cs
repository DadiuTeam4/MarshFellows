//Author: Troels
//Contributer: Emil
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkHandler : MonoBehaviour {

    Animator anim;

    Vector3 lFpos;
    Vector3 rFpos;

    Quaternion lFrot;
    Quaternion rFrot;

    float lFWeight;
    float rFWeight;

    Transform leftFoot;
    Transform rightFoot;

    public float offsetY;

    public float lookAtWeight;
    public float headWeight;
    public float bodyWeight;
    public float clampWeight;

    private Transform lookPos;
    public Transform defaultLookPos;

	void Start ()
    {
        anim = GetComponentInChildren<Animator>();

        leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);

        lFrot = leftFoot.rotation;
        rFrot = rightFoot.rotation;
	}

    public void LookForward()
    {
        lookPos = defaultLookPos;
        lookAtWeight = 1;
        headWeight = 1;
        bodyWeight = 0.3f;
        clampWeight = 0.5f;
    }

    public void LookAtTarget(Transform other)
    {
        lookPos = other;
        lookAtWeight = 1;
        headWeight = 1;
        bodyWeight = 0.3f;
        clampWeight = 0.5f;
    }

    void OnAnimatorIK()
    {

        if (lookPos)
        {
            anim.SetLookAtPosition(lookPos.position);
        } 
        anim.SetLookAtWeight(lookAtWeight, bodyWeight,headWeight, 0, clampWeight);
    }
}
