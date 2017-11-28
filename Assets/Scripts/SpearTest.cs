// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class SpearTest : Holdable 
{
	public CustomEvent calledEvent;
	
	public override void OnTouchBegin(RaycastHit hit)
	{
		EventManager.GetInstance().CallEvent(calledEvent);
	}
}
