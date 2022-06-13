using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hive_Manager : MonoBehaviour
{
	Game_Manager manager;

	private void Start()
	{
		manager = Game_Manager.Instance;
		manager.LoadScene();
	}

	public void onButtonClick()
	{
		Helper.changeScene(Helper.MAP);
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Helper.ExitGame();
		}
	}
}
