// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class OnTouchBeginParticleCaster : MonoBehaviour 
{
	private EventDelegate placeParticle;
	
	void Start () 
	{
		placeParticle = PlaceParticleSystem;
		//EventManager.GetInstance().AddListener(CustomEvent.TouchBegan, placeParticle);
	}
	
	private void PlaceParticleSystem(EventArgument argument)
	{

	}
}
