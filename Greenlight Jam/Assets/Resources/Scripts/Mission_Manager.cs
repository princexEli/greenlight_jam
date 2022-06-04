﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mission_Manager : MonoBehaviour
{
    #region instance
    private static Mission_Manager instance;
    public static Mission_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Mission_Manager>();
            }

            return instance;
        }
        set { instance = value; }
    }
    #endregion

    public List<string> items;
    List<Mission> activeMissions, availableMissions;
    GameObject[] missionUI;

    public int maxComponents = 3, maxItems = 5;
    int unlockedMissionSlots = 1;
    public void addMission(Mission m)
	{
        activeMissions.Add(m);
	}
    public void removeMission(Mission m)
    {
        activeMissions.Remove(m);
    }

    public void LoadPause()
	{
        foreach(Mission m in activeMissions)
		{
            Debug.Log("ah");
		}
	}

	private void Awake()
	{
        activeMissions = new List<Mission>();
        availableMissions = new List<Mission>();
    }

	public void LoadHive()
	{
        missionUI = GameObject.FindGameObjectsWithTag("Mission");
        
        if (maxComponents > items.Count)
		{
            Debug.LogWarning("maxComponent will be set to items.Count, since the value entered will create an infinate loop");
            maxComponents = items.Count;
		}

        for (int i = 0; i < missionUI.Length; i++)
		{
            bool isFirst = false;
            if(i == 0)
			{
                isFirst = true;
			}

            Mission temp = missionUI[i].GetComponent<Mission>();
            temp.setupMission(isFirst, (i >= unlockedMissionSlots));
            activeMissions.Add(temp);

            TextMeshProUGUI tmpro = missionUI[i].GetComponentInChildren<TextMeshProUGUI>();

            tmpro.text = temp.title;
        }
	}
}
