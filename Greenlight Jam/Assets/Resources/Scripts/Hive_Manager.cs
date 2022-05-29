using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hive_Manager : MonoBehaviour
{
    Mission_Manager missionManager;
    Upgrade_Manager upgradeManager;

	private void Awake()
	{
        missionManager = GetComponent<Mission_Manager>();
        upgradeManager = GetComponent<Upgrade_Manager>();
    }

	public void onButtonClick()
	{
		SceneManager.LoadScene("Ground");
	}
}
