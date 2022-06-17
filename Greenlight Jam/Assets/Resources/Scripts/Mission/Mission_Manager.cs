using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mission_Manager : Data_Manager
{
    int missionPos = 0;
    public GameObject activeMissions, availableMissions;

    int availableCount
	{
		get { return availableMissions.transform.childCount; }
	}
    int activeCount
    {
        get { return activeMissions.transform.childCount; }
    }

    public override void awake()
	{
        tagName = "Mission";
    }

    public override void Initalize()
    {
        availableMissions = new GameObject("Available Missions");
        availableMissions.transform.parent = gameObject.transform;

        activeMissions = new GameObject("Active Missions");
        activeMissions.transform.parent = gameObject.transform;
    }

    public override void attachHosts(GameObject[] hosts)
	{
        missionPos = 0;
        if (activeCount != 0 && Helper.SceneType() != Helper.MENU)
        {
            LoadActive(hosts);
        }
        if (Helper.SceneType() == "Hive")
		{
            CreateAvailable(hosts);
		}
        HideInactive(hosts);
    }

	private void HideInactive(GameObject[] hosts)
	{
        for (int i = missionPos; i < hosts.Length; i++)
		{
            hosts[i].SetActive(false);
        }
	}

	private void LoadActive(GameObject[] hosts) 
    {  
        for (int i = 0; i < activeCount; i++)
        {
            Mission temp = activeMissions.transform.GetChild(i).gameObject.GetComponent<Mission>();
            temp.addHost(hosts[i]);
            temp.Load();

            TextMeshProUGUI tmpro = hosts[i].GetComponentInChildren<TextMeshProUGUI>();

            tmpro.text = temp.ComponentsText;

            missionPos++;
        }
    }
    
    private void CreateAvailable(GameObject[] hosts) 
    {
        int size = Game_Manager.Instance.unlockedMissions-activeCount;

        for (int i = activeCount; i < size; i++)
        {
            bool isFirst = false;
            if (i == 0)
            {
                isFirst = true;
            }

            GameObject temp = new GameObject("Mission");
            if (isFirst) 
            { 
                temp.transform.parent = activeMissions.transform; 
            }   
            else { 
                temp.transform.parent = availableMissions.transform; 
            }
                
            Mission m = temp.AddComponent<Mission>();
            m.addHost(hosts[i]);
            m.Initalize(isFirst);
            m.Load();

            
            TextMeshProUGUI tmpro = hosts[i].GetComponentInChildren<TextMeshProUGUI>();

            tmpro.text = m.ComponentsText;
            missionPos ++;
        }

    }

    public void deleteAvailable()
	{
        foreach (Transform child in availableMissions.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

	public override void Load()
	{

	}
}
