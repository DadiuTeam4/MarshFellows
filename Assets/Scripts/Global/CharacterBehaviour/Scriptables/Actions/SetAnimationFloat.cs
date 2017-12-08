//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterBehaviour
{
    [CreateAssetMenu(menuName = "Character Behaviour/Actions/AnimationFloat")]
    public class SetAnimationFloat : Action
    {

        public string floatName;
        public float value;

        public override void Act(StateController controller)
        {
            controller.SetAnimatorFloat(floatName, value);
        }

    }
}
