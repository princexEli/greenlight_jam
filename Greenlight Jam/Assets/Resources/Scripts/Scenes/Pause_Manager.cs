using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Manager : MonoBehaviour
{
	Game_Manager manager;
	GameObject pauseMenu, isoMenu;
	bool isPaused = false;
	bool isLoaded = false;

	private void Start()
	{
		manager = Game_Manager.Instance;

		manager.audio.swapTheme();
		isoMenu = GameObject.Find("Iso UI");
		manager.inventory.BeginLoad();
		//manager.mission.LoadPause();
		manager.upgrade.LoadPause();
		pauseMenu = GameObject.Find("Pause UI");
		pauseMenu.SetActive(isPaused);

	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			OnContinueClick();
		}
	}

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
	public void OnSaveClick()
	{

	}
	public void OnSettingsClick()
	{

	}
	public void OnMainMenuClick()
	{
		Helper.changeScene(Helper.MENU);
	}
	public void OnQuitClick()
	{
		Helper.ExitGame();
	}
}
