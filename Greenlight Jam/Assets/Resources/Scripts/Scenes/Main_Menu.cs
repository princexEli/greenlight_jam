using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Main_Menu : MonoBehaviour
{
    Slider volumeSlider;

    public void onPlayClicked()
	{
		SceneManager.LoadScene("The Hive");
	}
	private void Update()
	{
		Helper.ExitGame();
	}
}
