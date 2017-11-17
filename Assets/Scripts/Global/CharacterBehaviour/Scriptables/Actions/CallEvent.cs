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

    public override void Act(StateController controller)
    {
        EventManager.GetInstance().CallEvent(callEvent);
    }

    private void ReactToEvent(StateController controller)
    {

    }
}