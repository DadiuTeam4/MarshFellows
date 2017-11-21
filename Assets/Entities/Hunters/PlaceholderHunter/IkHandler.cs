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

    public Transform lookPos;

	void Start ()
    {
        anim = GetComponentInChildren<Animator>();

        leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);

        lFrot = leftFoot.rotation;
        rFrot = rightFoot.rotation;
	}
	
	void Update ()
    {

        RaycastHit leftHit;
        RaycastHit rightHit;

        Vector3 lpos = leftFoot.TransformPoint(Vector3.zero);
        Vector3 rpos = rightFoot.TransformPoint(Vector3.zero);

        if (Physics.Raycast(lpos, -Vector3.up, out leftHit, 1))
        {
            lFpos = leftHit.point;
            lFrot = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
        }
        if (Physics.Raycast(rpos, -Vector3.up, out rightHit, 1))
        {
            rFpos = rightHit.point;
            rFrot = Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation;
        }
    }

    public void LookForward()
    {
        clampWeight = 0;
        lookAtWeight = 0;
        headWeight = 0;
        bodyWeight = 0.0f;
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

        //lFWeight = anim.GetFloat("LFoot");
        //rFWeight = anim.GetFloat("RFoot");
        lFWeight = 0;
        rFWeight = 0;

        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, lFWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, rFWeight);    
        anim.SetIKPosition(AvatarIKGoal.LeftFoot, lFpos + new Vector3(0,offsetY,0));
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rFpos + new Vector3(0, offsetY, 0));

        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, lFWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, rFWeight);
        anim.SetIKRotation(AvatarIKGoal.LeftFoot, lFrot);
        anim.SetIKRotation(AvatarIKGoal.RightFoot, rFrot);
    }


}
