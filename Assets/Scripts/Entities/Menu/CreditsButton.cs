// Author: Jonathan
// Contributers: Kristian ,You Wu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;

public class CreditsButton : MonoBehaviour
{
    public AudioManager audioManager;
    public Transform creditsPanel;

    void Start()
    {
        audioManager = AudioManager.GetInstance();
    }

    public void RunCredits()
    {
		creditsPanel.gameObject.SetActive(true);
		transform.parent.gameObject.SetActive(false);
        audioManager.OnMenuClick();
    }
}

