//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterBehaviour
{
    [CreateAssetMenu(menuName = "Character Behaviour/Actions/AnimationBool")]
    public class SetAnimationBool : Action {

        public string boolName;
        public bool value;

        public override void Act(StateController controller)
        {
            controller.SetAnimatorBool(boolName, value);
        }

    }
}