// Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterBehaviour
{
    [CreateAssetMenu(menuName = "Character Behaviour/Actions/NavigationUpdate")]
    public class NavigatorUpdate : Action
    {
        public bool setPreviousSpeed;
        public float speed;

        public override void Act(StateController controller)
        {
            if (setPreviousSpeed)
            {
                controller.navigator.SetSpeed(GlobalConstantsManager.GetInstance().constants.speed);
            } else
            {
                controller.navigator.SetSpeed(speed);
            }

        }

    }
}