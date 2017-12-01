// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class SetDestinationTrigger : MonoBehaviour 
{
	public string target;
	public Transform newDestination;
	public CustomEvent onEventToggleOff;
	public bool on;

	public bool setOther = false;
	public Navigator otherNavigator;

	private EventDelegate toggle;

	void Start()
	{
		toggle = Toggle;

		EventManager.GetInstance().AddListener(onEventToggleOff, toggle);
	}

	private void Toggle(EventArgument argument)
	{
		on = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (!on)
		{
			return;
		}

		if (other.transform.tag == target)
		{
			Navigator navigator = other.gameObject.GetComponent<Navigator>();

			if (navigator)
			{
				navigator.SetDestination(newDestination);

				if (setOther)
				{
					otherNavigator.SetDestination(newDestination);
				}
			}
		}	
	}
}
