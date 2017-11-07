//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveScript : MonoBehaviour {
GameStateManager newRound = new GameStateManager();

	void Update () {
		if (Input.GetKeyDown("n"))
		{
			newRound = GameStateManager.current;
			newRound.playedBefore = true;
			newRound.roundsPlayed++;
			GameStateManager.current = newRound;
			print("N Played before = "+GameStateManager.current.roundsPlayed);
			
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
