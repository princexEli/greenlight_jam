using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
	Locked_Panel lockPanel;
	private void Awake()
	{
		switch (Helper.SceneType())
		{
			case "Hive":
				lockPanel = gameObject.GetComponentInChildren<Locked_Panel>();
				break;
			default:
				break;
		}
	}

	public void setupUpgrade(bool isLocked)
	{
		lockPanel.enable = isLocked;
	}
}
