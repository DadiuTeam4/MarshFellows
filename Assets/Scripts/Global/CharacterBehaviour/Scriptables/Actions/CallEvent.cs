// Author: Emil Villumsen
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

[CreateAssetMenu(menuName = "Character Behaviour/Actions/CallEvent")]
public class CallEvent : Action
{

    public CustomEvent callEvent;
    public string stringArgument = "";

    public override void Act(StateController controller)
    {
        EventManager.GetInstance().CallEvent(callEvent, new EventArgument
        {
            stringComponent = stringArgument,
            intComponent = 1
        });
    }

    private void ReactToEvent(StateController controller)
    {

    }
}