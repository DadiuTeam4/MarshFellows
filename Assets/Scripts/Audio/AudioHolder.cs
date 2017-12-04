// Author: Kristian Riis 
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHolder : MonoBehaviour {

	public AudioManager audioManager; 

	// Use this for initialization
	void Awake () 
	{
		audioManager = AudioManager.GetInstance(); 	
	}
		
	void FSO()
	{
		audioManager.PlaySoundOtherScript("Play_FS_O", gameObject);
	}

	void FSP()
	{
		audioManager.PlaySoundOtherScript("Play_FS_P", gameObject);
	}

	void DeerFS()
	{
		audioManager.PlaySoundOtherScript ("Play_Deer_FS", gameObject); 
	}

	void BearFS()
	{
		audioManager.PlaySoundOtherScript ("Play_Bear_FS", gameObject); 
	}

	void BearFSlight()
	{
		audioManager.PlaySoundOtherScript ("Play_Bear_FS_Light", gameObject); 
	}

	void BearFsStomp()
	{
		audioManager.PlaySoundOtherScript ("Play_Bear_FS_Stomp", gameObject); 
	}
		
	void BearRoarTrans()
	{
		audioManager.PlaySoundOtherScript ("Play_GG_SD_Bear_roar_loud_1", gameObject); 
	}

	void BearBreath()
	{
		audioManager.PlaySoundOtherScript ("Play_GG_SD_Bear_breath", gameObject); 
	}

	void BearLightRoar()
	{
		audioManager.PlaySoundOtherScript ("Play_Bear_SD_LightRoar", gameObject); 
	}

	void Transformation()
	{
		audioManager.PlaySoundOtherScript ("Play_GG_SD_ManToBear_1", gameObject); 
	}

	void ThrowSpear()
	{
		audioManager.PlaySoundOtherScript ("Play_GG_SD_JavelinWoosh", gameObject); 
	}

	void BowShoot()
	{
		audioManager.PlaySoundOtherScript ("Play_Bowpullshoot", gameObject); 
	}
}