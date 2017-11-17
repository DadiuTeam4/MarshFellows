// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebugPanel : MonoBehaviour 
{
	public GameObject debugPanel;
	private GameObject instance;

	public void SpawnPanel() 
	{
		if (!instance)
		{
			instance = Instantiate(debugPanel);
		}
		else 
		{
			instance.SetActive(!instance.activeSelf);
		}
	}
}
