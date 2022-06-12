using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
	Locked_Panel lockPanel;
	private void Awake()
	{
		switch (Helper.SceneName())
		{
			case "The Hive":
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
