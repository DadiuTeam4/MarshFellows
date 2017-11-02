// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour 
{	
	Image image;
	public Image buttonImage;
	public Text buttonText;
	public Text text;

	void Start () 
	{
		image = GetComponent<Image>();
		image.canvasRenderer.SetAlpha(0.0f);
		image.CrossFadeAlpha(1.0f, 2.0f, false);

		buttonImage.canvasRenderer.SetAlpha(0.0f);
		buttonImage.CrossFadeAlpha(1.0f, 4.0f, false);

		buttonText.canvasRenderer.SetAlpha(0.0f);
		buttonText.CrossFadeAlpha(1.0f, 4.0f, false);

		text.canvasRenderer.SetAlpha(0.0f);
		text.CrossFadeAlpha(1.0f, 3.0f, false);
	}
	
	public void StartGame()
	{
		SceneManager.LoadScene("GlobalScene");
	}
}
