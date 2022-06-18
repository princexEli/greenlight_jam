using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		manager.Load();
		Load();
	}

	private void Load()
	{
		switch (type)
		{
			case Helper.MENU:
				LoadMenu();
				break;
			case Helper.HIVE:
				LoadHive();
				break;
			case Helper.MAP:
			case Helper.PAUSE:
				LoadPause();
				break;
			case Helper.SUMMARY:
				LoadSummary();
				break;
		}
	}
	
	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (type == Helper.HIVE || type == Helper.MENU || type == Helper.SUMMARY)
			{
				Helper.ExitGame();
			}
			else
			{
				pause_OnContinueClick();
			}
		}
	}

	#region Hive
	private void LoadHive()
	{
		Button button = GameObject.Find("Launch Button").gameObject.GetComponent<Button>();
		button.onClick.AddListener(delegate () { hive_onDiveButtonClick(); });
	}
	public void hive_onDiveButtonClick()
	{
		Game_Manager.Instance.mission.deleteAvailable();
		Helper.changeScene(Helper.MAP);
	}
	#endregion

	#region Pause Menu
	private void LoadPause()
	{
		isoMenu = GameObject.Find("Iso UI");
		pauseMenu = GameObject.Find("Pause UI");

		Button button = GameObject.Find("Continue Button").gameObject.GetComponent<Button>();
		button.onClick.AddListener(delegate () { pause_OnContinueClick(); });
		button = GameObject.Find("Quit Button").gameObject.GetComponent<Button>();
		button.onClick.AddListener(delegate () { pause_OnQuitClick(); });


		pauseMenu.SetActive(isPaused);
	}
	public void pause_OnContinueClick()
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
	public void pause_OnQuitClick()
	{
		Helper.ExitGame();
	}
	#endregion

	#region Main Menu
	private void LoadMenu()
	{
		Button button = GameObject.Find("Play Button").gameObject.GetComponent<Button>();
		button.onClick.AddListener(delegate () { mainMenu_onPlayClicked(); });
	}
	public void mainMenu_onPlayClicked()
	{
		Helper.changeScene("Hive");
	}
	#endregion

	#region Summary
	private void LoadSummary()
	{
		Button button = GameObject.Find("Continue Button").gameObject.GetComponent<Button>();
		button.onClick.AddListener(delegate () { summary_OnContinueClick(); });
	}

	private void summary_OnContinueClick()
	{
		Helper.changeScene(Helper.HIVE);
	}
	#endregion
}
