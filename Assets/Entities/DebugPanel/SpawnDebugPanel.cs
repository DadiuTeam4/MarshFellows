// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebugPanel : MonoBehaviour 
{
	public GameObject debugPanel;

	public void SpawnPanel() 
	{
		if (!debugPanel)
		{
			Instantiate(debugPanel);
		}
		else 
		{
			debugPanel.SetActive(!debugPanel.active);
		}
	}
}
