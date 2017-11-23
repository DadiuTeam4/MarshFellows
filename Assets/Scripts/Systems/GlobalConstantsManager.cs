// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[ExecuteInEditMode]
public class GlobalConstantsManager : Singleton<GlobalConstantsManager> 
{
	public GlobalConstants constants;
	
	[Header("Update Button (used in editor)")]
	public bool update = false;

	public void UpdateSettings()
	{
		RenderSettings.fog = constants.on;
		RenderSettings.fogColor = constants.fogColor;
		RenderSettings.fogDensity = constants.fogDensity;
		RenderSettings.fogMode = constants.fogMode;

		update = false;
	}

	public void Update()
	{
		if (Application.isEditor && update)
		{
			UpdateSettings();
		}
	}
}
