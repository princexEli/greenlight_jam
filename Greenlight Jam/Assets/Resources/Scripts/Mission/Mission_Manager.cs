using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mission_Manager : Data_Manager
{
    int currMission = 0;

    List<Mission> activeList, availableList;
    GameObject activeMissions, availableMissions;

    public override void awake()
	{
        tagName = "Mission";
        
        activeList = new List<Mission>();
        availableList= new List<Mission>();
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
        if (activeList.Count != 0)
        {
            LoadActive(hosts);
        }
        if (Helper.SceneType() == "Hive")
		{
            CreateAvailable(hosts);
		}
        
    }

    private void LoadActive(GameObject[] hosts) { }
    
    private void CreateAvailable(GameObject[] hosts) 
    {
        int size = hosts.Length;
        if (hosts.Length > unlocked)
        {
            size = unlocked;
        }

        for (int i = 0; i < size; i++)
        {
            bool isFirst = false;
            if (i == 0)
            {
                isFirst = true;
            }
           
            Mission temp = new Mission();
            temp.Initalize(isFirst);

            availableList[i].addHost(hosts[i]);
            availableList[i].Load();

            if (isFirst)
                activeList.Add(temp);
            TextMeshProUGUI tmpro = hosts[i].GetComponentInChildren<TextMeshProUGUI>();

            tmpro.text = temp.ComponentsText;
        }
    }




    public void LoadPause()
	{
        foreach(Mission a in activeList)
		{
            a.loadMission();
        }
    }

 
    public Mission getNextMission()
	{
        if (currMission == activeList.Count) return null;
        Mission m = (Mission)activeList[0];
        currMission++;
        return m;
	}

	public override void Load()
	{
	}
}
