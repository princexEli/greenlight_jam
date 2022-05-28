using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    bool isLocked = true;
	Locked_Panel lockPanel;

	private void Awake()
	{
		lockPanel = gameObject.GetComponentInChildren<Locked_Panel>();
		Debug.Log(lockPanel);
	}

	public void setupUpgrade(bool isLocked)
	{
		lockPanel.enable = isLocked;
	}
}
