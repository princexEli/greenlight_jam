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
		missionManager.LoadHive();
		upgradeManager = Upgrade_Manager.Instance;
		upgradeManager.LoadHive();
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
