// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Character Behaviour/State")]
public class State : ScriptableObject
{
	public Action[] actions;
	public Transition[] transitions;

	public void UpdateState(StateController controller)
	{
		DoActions(controller);
		CheckTransitions(controller);
    }



	private void DoActions(StateController controller)
	{
		foreach (Action action in actions) 
		{
			action.Act(controller);
		}
	}

	private void CheckTransitions(StateController controller)
	{
		foreach (Transition transition in transitions)
		{
			bool decisionSucceeded = transition.decision.Decide(controller);
            // Debug.Log("Transition to " + transition.trueState + " " + decisionSucceeded);

            if (decisionSucceeded)
			{
			    controller.TransitionToState(transition.trueState);
                break;
			}
			else
			{
				controller.TransitionToState(transition.falseState);
			}
		}
	}
}
