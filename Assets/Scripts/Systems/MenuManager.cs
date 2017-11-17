// Author: Itai Yavin
// Contributors: Kristian Riis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private bool portrait;
    public AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.GetInstance();
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            portrait = true;
        }
    }

    void Update()
    {
        if (IspPortraitOrPortraitUpsideDown() && !portrait)
        {
            portrait = true;
            Time.timeScale = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (!transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).name != "CreditsPanel")
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                    audioManager.MenuFadeSoundDown();
                }
            }
        }
        else if (!IspPortraitOrPortraitUpsideDown() && portrait)
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
    }

    private bool IspPortraitOrPortraitUpsideDown()
    {
        return (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown 
				|| Input.deviceOrientation == DeviceOrientation.FaceDown || Input.deviceOrientation == DeviceOrientation.FaceUp);
    }
}
