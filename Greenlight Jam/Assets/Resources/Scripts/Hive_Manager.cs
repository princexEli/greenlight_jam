using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hive_Manager : MonoBehaviour
{
    Mission_Manager missionManager;
    Upgrade_Manager upgradeManager;

	private void Start()
	{
		missionManager = Mission_Manager.Instance;
		upgradeManager = Upgrade_Manager.Instance;
		if (Helper.SceneType() == "The Hive")
		{
			missionManager.LoadHive();
			upgradeManager.LoadHive();
		}
		else if(Helper.SceneType() != "Main Menu") 
		{
			missionManager.LoadPause();
			upgradeManager.LoadPause();
		}
		
	}

	public void onButtonClick()
	{
		SceneManager.LoadScene("Ground");
	}

	private void Update()
	{
		Helper.ExitGame();
	}
}
