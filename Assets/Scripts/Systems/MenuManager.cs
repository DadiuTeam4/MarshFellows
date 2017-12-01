// Author: Itai Yavin
// Contributors: Kristian Riis,Tilemachos

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public bool portrait;
    public AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.GetInstance();

        if (IspPortraitOrPortraitUpsideDown())
        {
            showMenuAndPauseGame();
        }
        else
        {
            portrait = false;
        }
    }

    void Update()
    {
        if (IspPortraitOrPortraitUpsideDown() && !portrait)
        {
            showMenuAndPauseGame();
        }
        else if (IsLandscape())
        {
            ResumeGameAndDisableMenu();
        }
    }

    private void showMenuAndPauseGame()
    {
        portrait = true;
        Time.timeScale = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            string childName = transform.GetChild(i).name;
            if (!transform.GetChild(i).gameObject.activeSelf && childName != "CreditsPanel" && childName != "AudioPanel")
            {
                transform.GetChild(i).gameObject.SetActive(true);
                audioManager.MenuFadeSoundDown();
            }
        }
    }

    private void ResumeGameAndDisableMenu()
    {
        portrait = false;
        Time.timeScale = 1;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                audioManager.MenuFadeSoundUp();
            }
        }
    }

    private bool IspPortraitOrPortraitUpsideDown()
    {
        return (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown);
    }


    private bool IsLandscape()
    {
        return (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight);
    }
}
