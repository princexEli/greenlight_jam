using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using System;

public class Exit : MonoBehaviour
{
    public bool isLevelExit;
	public Transform newLocation;
	Outline highlight;

	private void Awake()
	{
		highlight = gameObject.AddComponent<Outline>();
		highlight.
		gameObject.tag = "EntranceExit";
		if(!isLevelExit && newLocation == null)
		{
			Debug.LogError("New location not set.");
		}
	}
	private void Start()
	{
		highlight.enabled = false; 
	}

	public void teleport()
	{
		if (isLevelExit)
		{
			Helper.changeScene(Helper.SUMMARY);
		}
		else
		{
			Character_Manager.Instance.teleport(newLocation);
		}
	}
	internal void Highlight(bool isActive)
	{
		try
		{
			highlight.enabled = isActive;
		}
		catch (Exception ex)
		{
			Debug.LogError("Failure to set highlight to " + isActive + ". " + ex);
		}
	}
}
