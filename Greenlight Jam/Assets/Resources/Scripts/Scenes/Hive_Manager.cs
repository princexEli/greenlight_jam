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
		if (Mission_Manager.Instance == null)
		{
			Helper.setupTest();
		}
		Audio_Manager.Instance.swapTheme();
		missionManager = Mission_Manager.Instance;
		upgradeManager = Upgrade_Manager.Instance;
		Inventory_Manager.Instance.Load();

		switch (Helper.SceneType())
		{
			case "Hive":
				Audio_Manager.Instance.swapTheme();
				missionManager.LoadHive();
				upgradeManager.LoadHive();
				
				break;
			case "Menu":
				missionManager.LoadPause();
				upgradeManager.LoadPause();
				break;
			default: break;
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
