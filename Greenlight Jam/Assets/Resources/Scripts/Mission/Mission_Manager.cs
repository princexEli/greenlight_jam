using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mission_Manager : Data_Manager
{
    List<Mission> activeList, availableList;
    public int maxComponents = 3, maxItems = 5;
    int unlockedMissionSlots = 1;

	public override void Setup()
	{
        tagName = "Mission";
        activeList = new List<Mission>();
        availableList= new List<Mission>();
    }
	public void LoadPause()
	{
        foreach(Mission a in activeList)
		{
            a.loadMission();
        }
    }

    int currMission = 0;
    public Mission getNextMission()
	{
        if (currMission == activeList.Count) return null;
        Mission m = (Mission)activeList[0];
        currMission++;
        return m;
	}

	public void LoadHive()
	{
        hosts = GameObject.FindGameObjectsWithTag("Mission");
        
        if (maxComponents > Game_Manager.Instance.lootTypes.Length)
		{
            Debug.LogWarning("maxComponent will be set to items.Count, since the value entered will create an infinate loop");
            maxComponents = Game_Manager.Instance.lootTypes.Length;
		}

        for (int i = 0; i < hosts.Length; i++)
		{
            bool isFirst = false;
            if(i == 0)
			{
                isFirst = true;
			}

            Mission temp = hosts[i].GetComponent<Mission>();
            temp.Initalize(isFirst, (i >= unlockedMissionSlots));
            availableList.Add(temp);
            if (isFirst)
                activeList.Add(temp);
            TextMeshProUGUI tmpro = hosts[i].GetComponentInChildren<TextMeshProUGUI>();

            tmpro.text = temp.title;
        }
	}
}
