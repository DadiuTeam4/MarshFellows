// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Behaviour/Actions/Walk")]
public class Walk : Action
{

    public bool changePath;

    public override void Act(StateController controller)
    {
        WalkTowards(controller);
    }

    private void WalkTowards(StateController controller)
    {

            if (!changePath)
            {
                controller.navigator.SetDestination();
            } else
            {
                controller.navigator.SetSplitPath();
            }

    }
}