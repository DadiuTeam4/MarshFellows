// Author: Itai Yavin
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

[CreateAssetMenu(menuName = "Character Behaviour/Decisions/WithinDistanceFromDestination")]
public class WithinDistanceFromDestination : Decision
{
    public float wantedDistance;

	public override bool Decide(StateController controller) 
	{
        float currentDistance = Vector3.Distance(controller.transform.position, controller.GetDestination().position);
        if (currentDistance <= wantedDistance)
        {
            return true;
        }

        return false;
	}
}
