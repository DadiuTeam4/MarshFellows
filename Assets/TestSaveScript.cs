//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveScript : MonoBehaviour {
GameStateManager newRound = new GameStateManager();
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("n"))
		{
			
			newRound.playedBefore = true;
			newRound.roundsPlayed++;
			GameStateManager.current = newRound;
			print("N Played before = "+newRound.roundsPlayed);
			
		}
		if (Input.GetKeyDown("s"))
		{
			SaveLoadManager.Save();
		}

		if (Input.GetKeyDown("l"))
		{
			SaveLoadManager.Load();
			print("L Played before = "+GameStateManager.current.roundsPlayed);
		}
	}
}
