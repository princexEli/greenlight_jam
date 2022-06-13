using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Main_Menu : MonoBehaviour
{
    Slider volumeSlider;

	private void Awake()
	{
		volumeSlider = gameObject.transform.GetComponentInChildren<Slider>();
	}
	public void onPlayClicked()
	{
		Helper.changeScene("Hive");
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Helper.ExitGame();
		}
	}
}
