// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
	public Transform menuPanel;
    public void Back()
    {
		menuPanel.gameObject.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
