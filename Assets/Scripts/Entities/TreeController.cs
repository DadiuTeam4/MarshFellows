// Author: You Wu
// Contributors: Jonathan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : Shakeable

{
    public float thresholdForTreeFallDown = 3200f;

    public float treeFallForceMin = -0.05f;
    public float treeFallForceMax = 0.05f;

    private Rigidbody treeRd;

	private bool isTreeFallen;

    void Awake()
    {
        treeRd = GetComponent<Rigidbody>();
    }

	void Start()
	{
		isTreeFallen = false;
	}

    public override void OnShakeBegin(float magnitude)
    {
        checkTreeFallDown(magnitude);
    }

    public override void OnShake(float magnitude)
    {
		checkTreeFallDown(magnitude);
    }

    private void checkTreeFallDown(float magnitude)
    {
        if (magnitude > thresholdForTreeFallDown && isTreeFallen == false)
        {
			Vector3 randomForce = GetShakeForceOnShakebleObject(magnitude);
            treeRd.AddForce(new Vector3(Random.Range(treeFallForceMin,treeFallForceMax), 0f, Random.Range(treeFallForceMin,treeFallForceMax)));
			isTreeFallen = true;
        }

    }

}
