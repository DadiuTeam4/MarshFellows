// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour 
{
	private bool portrait;

	void Start () 
	{	
		if (Input.deviceOrientation == DeviceOrientation.Portrait)
		{
			portrait = true;
		}
	}
	
	void Update () 
	{	
		if (IspPortraitOrPortraitUpsideDown() && !portrait)
		{
			portrait = true;
			Time.timeScale = 0;
			for (int i = 0; i < transform.childCount; i++)
			{
				if (!transform.GetChild(i).gameObject.activeSelf)
				{
					transform.GetChild(i).gameObject.SetActive(true);
				}
			}
		}
		else if(!IspPortraitOrPortraitUpsideDown() && portrait) 
		{
			portrait = false;
			Time.timeScale = 1;

			for(int i = 0; i < transform.childCount; i++)
			{
				if(transform.GetChild(i).gameObject.activeSelf)
				{
					transform.GetChild(i).gameObject.SetActive(false);
				}
			}
		}
	}

	private bool IspPortraitOrPortraitUpsideDown()
	{
		return (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown); 
	}
}
