// Author: Itai Yavin
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace CharacterBehaviour
{
    [CreateAssetMenu(menuName = "Character Behaviour/Actions/CallEventWithFoundGameObject")]
    public class CallEventWithFoundGameObject : Action
    {
        public CustomEvent callEvent;
        public string targetTag;
        public string owner;

        private EventArgument argument = new EventArgument();

        public override void Act(StateController controller)
        {
            argument.gameObjectComponent = GameObject.FindGameObjectWithTag(targetTag);
            argument.stringComponent = owner;

            if (argument.gameObjectComponent)
            {
                EventManager.GetInstance().CallEvent(callEvent, argument);
            }
        }
    }
}