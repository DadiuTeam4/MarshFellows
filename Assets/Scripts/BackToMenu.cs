// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
	public Transform menuPanel;
    public AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.GetInstance();
    }
    public void Back()
    {
		menuPanel.gameObject.SetActive(true);
        transform.parent.gameObject.SetActive(false);
        audioManager.OnMenuClick();
    }
}
