// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterBehaviour
{
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
            controller.navigator.ResumeMovement();

            if (!changePath)
            {
                controller.navigator.SetDestination();
            } else
            {
                controller.navigator.SetSplitPath();
            }

        }
    }
}