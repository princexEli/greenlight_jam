using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene_Manager : MonoBehaviour
{
	Game_Manager manager;
	GameObject pauseMenu, isoMenu;
	Slider volumeSlider;
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
				volumeSlider = gameObject.transform.GetComponentInChildren<Slider>();
				volumeSlider.onValueChanged.AddListener(delegate { Game_Manager.Instance.audio.updateVolume(volumeSlider.value); });
				break;
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
			if (type == Helper.HIVE || type ==Helper.MENU)
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

	#region Main Menu
	public void mainMenu_onPlayClicked()
	{
		Helper.changeScene("Hive");
	}
	#endregion
}
