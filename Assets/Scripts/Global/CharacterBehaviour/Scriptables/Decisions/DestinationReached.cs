// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace CharacterBehaviour
{
    [CreateAssetMenu(menuName = "Character Behaviour/Decisions/DestinationReached")]
    public class DestinationReached : Decision
    {

        public CustomEvent fireIfTrue = CustomEvent.None;

        public override bool Decide(StateController controller) 
        {
            bool result = controller.navigator.CheckDestinationReached();
            if (result)
            {
                controller.eventManager.CallEvent(fireIfTrue);
            }
            return result;
        }
    }
}