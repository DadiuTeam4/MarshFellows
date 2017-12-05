// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageButton : MonoBehaviour
{
    private AudioManager audioManager;
    private bool isEnglish;

    //Sprites
    public Sprite audiOSpriteEnglish;
    public Sprite audiOSpriteDanish;
    public Sprite restartSpriteEnglish;
    public Sprite restartSpriteDanish;
    public Sprite exitSpriteEnglish;
    public Sprite exitSpriteDanish;
    public Sprite languageSpriteEnglish;
    public Sprite languageSpriteDanish;

    public Sprite audiOTextSpriteEnglish;
    public Sprite audiOTextSpriteDanish;
    public Sprite sfxSpriteEnglish;
    public Sprite sfxSpriteDanish;
    public Sprite musicEnglish;
    public Sprite musicDanish;

    //Images
    private Image audioImage;
    private Image restartImage;
    private Image exitImage;
    private Image languageImage;
    public Image audioTextImage;
    public Image sfxImage;
    public Image musicImage;

    void Start()
    {
        audioManager = AudioManager.GetInstance();
        isEnglish = true;
        GetImagesComponents();

    }

    private void GetImagesComponents()
    {
        audioImage = GameObject.Find("Audio").GetComponent<Image>();
        restartImage = GameObject.Find("ResetButton").GetComponent<Image>();
        exitImage = GameObject.Find("Exit").GetComponent<Image>();
        languageImage = GetComponent<Image>();
    }

    public void OnLanguageChange()
    {
        ChangeImages();
        audioManager.OnMenuClick();

    }

    private void ChangeImages()
    {
        if (isEnglish)
        {
            isEnglish = false;
            audioImage.sprite = audiOSpriteDanish;
            audioTextImage.sprite = audiOTextSpriteDanish;
            restartImage.sprite = restartSpriteDanish;
            exitImage.sprite = exitSpriteDanish;
            languageImage.sprite = languageSpriteEnglish;
            sfxImage.sprite = sfxSpriteDanish;
            musicImage.sprite = musicDanish;
        }
        else
        {
            isEnglish = true;
            audioImage.sprite = audiOSpriteEnglish;
            restartImage.sprite = restartSpriteEnglish;
            exitImage.sprite = exitSpriteEnglish;
            languageImage.sprite = languageSpriteDanish;
            audioTextImage.sprite = audiOTextSpriteEnglish;
            sfxImage.sprite = sfxSpriteEnglish;
            musicImage.sprite = musicEnglish;
        }
    }

}
