﻿// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Character Behaviour/State")]
public class State : ScriptableObject
{
	public Action[] onStateEnterActions;
	public Action[] actions;
	public Action[] onStateExitActions;
	public Transition[] transitions;

	public void OnStateEnter(StateController controller)
	{
		DoOnStateEnterActions(controller);
	}

	private void DoOnStateEnterActions(StateController controller)
	{
		foreach (Action action in onStateEnterActions)
		{
			action.Act(controller);
		}
	}

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

	public void OnStateExit(StateController controller)
	{
		DoOnStateExitActions(controller);
	}

	private void DoOnStateExitActions(StateController controller)
	{
		foreach (Action action in onStateExitActions)
		{
			action.Act(controller);
		}
	}
}
