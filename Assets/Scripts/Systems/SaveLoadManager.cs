﻿//Author:Tilemachos
//Co-author:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public static class SaveLoadManager {
    public static GameStateManager savedGame = new GameStateManager();
	public static void Save() {
		savedGame = GameStateManager.current;
		if(savedGame != null)
		{
			savedGame.playedBefore = true;
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd");
			bf.Serialize(file, SaveLoadManager.savedGame);
			file.Close();
		}
	}

	public static void Load() {
		Debug.Log("Saved file at: " + Application.persistentDataPath);
		
    if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
        // Uncomment the line below to have the debugger print where your save/load file is located. Look for "savedGames.gd"
		SaveLoadManager.savedGame = (GameStateManager)bf.Deserialize(file);
		GameStateManager.current = SaveLoadManager.savedGame;
        file.Close();
    }
}

}
