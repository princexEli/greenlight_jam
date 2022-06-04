using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
	Locked_Panel lockPanel;
	private void Awake()
	{
		string scene = Helper.SceneName();

		switch (scene)
		{
			case "The Hive":
				lockPanel = gameObject.GetComponentInChildren<Locked_Panel>();
				break;
			default:
				Debug.Log("Current Scene '" + scene + "' has no upgrade loadouts.");
				break;
		}
	}

	public void setupUpgrade(bool isLocked)
	{
		lockPanel.enable = isLocked;
	}
}
