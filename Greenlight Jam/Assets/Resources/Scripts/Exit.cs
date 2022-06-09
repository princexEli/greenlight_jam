using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour
{
    public bool isLevelExit;
	public Transform newLocation;

	private void Awake()
	{
		if(!isLevelExit && newLocation == null)
		{
			Debug.LogError("New location not set.");
		}
	}

	public void teleport()
	{
		if (isLevelExit)
		{
			SceneManager.LoadScene("The Hive");
		}
		else
		{
			Character_Manager.Instance.teleport(newLocation);
		}
	}
}
