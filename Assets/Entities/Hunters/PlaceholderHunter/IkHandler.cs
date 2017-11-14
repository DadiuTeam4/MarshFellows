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

    public float lookIKweight;
    public float headWeight;
    public float bodyWeight;
    public float clampWeight;
    

    public Transform lookPos;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();

        leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);

        lFrot = leftFoot.rotation;
        rFrot = rightFoot.rotation;

	}
	
	// Update is called once per frame
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

    void OnAnimatorIK()
    {
        anim.SetLookAtWeight(lookIKweight, bodyWeight,headWeight, clampWeight);
        anim.SetLookAtPosition(lookPos.position);




        lFWeight = anim.GetFloat("LFoot");
        rFWeight = anim.GetFloat("RFoot");

        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, lFWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, rFWeight);
        
        anim.SetIKPosition(AvatarIKGoal.LeftFoot, lFpos + new Vector3(0,offsetY,0));
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rFpos + new Vector3(0, offsetY, 0));

        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, lFWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, rFWeight);

        anim.SetIKRotation(AvatarIKGoal.LeftFoot, lFrot);
        anim.SetIKRotation(AvatarIKGoal.RightFoot, rFrot);


    /*
        anim.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, ikWeight);
        anim.SetIKHintPositionWeight(AvatarIKHint.RightKnee, ikWeight);

        anim.SetIKHintPosition(AvatarIKHint.LeftKnee, hintLeft.position);
        anim.SetIKHintPosition(AvatarIKHint.RightKnee, hintRight.position);
    */

    }


}
