using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSerializable : MonoBehaviour
{
	int currentLevel;
	float volume;
	bool boolToSave;

	void SaveGame()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath
					 + "/MySaveData.dat");
		SaveData data = new SaveData();
		data.currentLevel = currentLevel;
		data.volume = volume;
		data.savedBool = boolToSave;
		bf.Serialize(file, data);
		file.Close();
		Debug.Log("Game data saved!");
	}

	void LoadGame()
	{
		if (File.Exists(Application.persistentDataPath
					   + "/MySaveData.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file =
					   File.Open(Application.persistentDataPath
					   + "/MySaveData.dat", FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize(file);
			file.Close();
			currentLevel = data.currentLevel;
			volume = data.volume;
			boolToSave = data.savedBool;
			Debug.Log("Game data loaded!");
		}
		else
			Debug.LogError("There is no save data!");
	}

	void ResetData()
	{
		if (File.Exists(Application.persistentDataPath
					  + "/MySaveData.dat"))
		{
			File.Delete(Application.persistentDataPath
							  + "/MySaveData.dat");
			currentLevel = 0;
			volume = 0.0f;
			boolToSave = false;
			Debug.Log("Data reset complete!");
		}
		else
			Debug.LogError("No save data to delete.");
	}
}

[Serializable]
class SaveData
{
    public int currentLevel;
    public float volume;
    public bool savedBool;
}
