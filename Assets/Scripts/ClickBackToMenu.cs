// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBackToMenu : MonoBehaviour
{

    public Transform menuPanel;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.GetInstance();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Back();
        }
    }
    public void Back()
    {
        menuPanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
        audioManager.OnMenuClick();
    }
}
