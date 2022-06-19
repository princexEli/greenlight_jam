using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using System;

public class Exit : MonoBehaviour
{
    public bool isLevelExit;
	Transform newLocation;
	Outline highlight;

	private void Awake()
	{
		highlight = gameObject.AddComponent<Outline>();
		gameObject.tag = "EntranceExit";
		if(!isLevelExit && newLocation == null)
		{
			GameObject[] temp = GameObject.FindGameObjectsWithTag("Teleport");
			foreach(GameObject t in temp)
			{
				if(t.name == gameObject.name)
				{
					newLocation = t.transform;
					break;
				}
			}
			if(newLocation == null)
			{
				Debug.LogError("Could not find gameobject called " + name + " that also has the teleport tag.");
			}
			
		}
		else
		{
			Destroy(gameObject);
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
