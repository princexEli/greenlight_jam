﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Manager : MonoBehaviour
{
	Mission_Manager missionManager;
	Upgrade_Manager upgradeManager;
	Inventory_Manager inventoryManager;
	GameObject pauseMenu, isoMenu;
	bool isPaused = false;
	bool isLoaded = false;

	private void Start()
	{
		if (Mission_Manager.Instance == null)
		{
			Helper.setupTest();
		}
		Audio_Manager.Instance.swapTheme();
		isoMenu = GameObject.Find("Iso UI");
		inventoryManager = Inventory_Manager.Instance;
		inventoryManager.Load();
		missionManager = Mission_Manager.Instance;
		missionManager.LoadPause();
		upgradeManager = Upgrade_Manager.Instance;
		upgradeManager.LoadPause();
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