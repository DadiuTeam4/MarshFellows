// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Behaviour/Actions/Walk")]
public class Walk : Action
{
    public bool walkToWaypoint = true;
    public bool walkInDirection;
    public Vector3 direction;
    public bool changePath;

    public override void Act(StateController controller)
    {
        WalkTowards(controller);
    }

    private void WalkTowards(StateController controller)
    {

        if (walkToWaypoint)
        {
            if (!changePath)
            {
                controller.navigator.SetDestination();
            } else
            {
                controller.navigator.SetSplitPath();
            }
        }
        if (walkInDirection)
        {
            controller.navigator.Move(direction);
        }
    }
}