// Author: Peter Jæger
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutScene : MonoBehaviour 
{

	// Use this for initialization
	public Animator anim;
	void Start () {
	anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat("deerSpeed", 200.0f);
	}
}
