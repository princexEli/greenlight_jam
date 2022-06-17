using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Manager : MonoBehaviour
{
	Game_Manager manager;
	GameObject pauseMenu, isoMenu;
	string type;
	bool isPaused = false;

	private void Start()
	{
		manager = Game_Manager.Instance;
		type = Helper.SceneType();
		Load();
		manager.Load();
		
	}

	private void Load()
	{
		switch (type)
		{
			case Helper.HIVE: break;
			case Helper.MAP:
			case Helper.PAUSE:
				isoMenu = GameObject.Find("Iso UI");
				pauseMenu = GameObject.Find("Pause UI");
				pauseMenu.SetActive(isPaused);
				break;
		}
	}
	

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (type == Helper.HIVE)
			{
				Helper.ExitGame();
			}
			else
			{
				OnContinueClick();
			}
		}
	}

	#region Hive
	public void onDiveButtonClick()
	{
		Helper.changeScene(Helper.MAP);
	}
	#endregion

	#region Pause Menu
	public void OnContinueClick()
	{
		isPaused = !isPaused;
		if (!isPaused)
		{
			Time.timeScale = 1;
		}
		else
		{
			Time.timeScale = 0;
		}
		pauseMenu.SetActive(isPaused);
		isoMenu.SetActive(!isPaused);
	}
	public void OnMainMenuClick()
	{
		Helper.changeScene(Helper.MENU);
	}
	public void OnQuitClick()
	{
		Helper.ExitGame();
	}
	#endregion
}
