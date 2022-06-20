using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
	TextMeshProUGUI displayL, displayS;
	float timeLimit;
	AudioSource endingSound, endSound;
	Scene_Manager manager;
	private void Awake()
	{
		if (Helper.SceneType() != Helper.PAUSE) Destroy(gameObject);
		displayL = GameObject.Find("Large Text").GetComponent<TextMeshProUGUI>();
		displayS = GameObject.Find("Small Text").GetComponent<TextMeshProUGUI>();
		endSound = gameObject.AddComponent<AudioSource>();
		endSound.clip = Game_Manager.Instance.timerSound;
		endingSound = gameObject.AddComponent<AudioSource>();
		endingSound.clip = Game_Manager.Instance.countDownSound;
	}

	private void Start()
	{
		timeLimit = Game_Manager.Instance.timeLimit + Game_Manager.Instance.upgrade.max("Timer")*Game_Manager.Instance.levelMultiplier;
		countdown = Game_Manager.Instance.countdown;
		manager = GameObject.Find("Ground UI Canvas").GetComponent<Scene_Manager>();
	}

	int countdown;
	int i = 0;
	private void Update()
	{
		timeLimit -= Time.deltaTime;
		string[] truncatedNum = (System.Math.Truncate(timeLimit * 100) / 100).ToString().Split('.');
		displayL.text = truncatedNum[0];
		if(truncatedNum.Length>1)
			displayS.text = truncatedNum[1];

		if (timeLimit < 0)
		{
			endSound.Play();
			Time.timeScale = 0;
			manager.TimesUp();
			
		}
		else if (timeLimit < (float)countdown)
		{
			endingSound.Play();
			countdown--;
		}
		
		i++;
    }
}
