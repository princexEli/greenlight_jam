using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mission_Manager : Data_Manager
{
    public List<Mission> missions;
    List<string> types;

    public override void awake()
	{
        missions = new List<Mission>();
        tagName = "Mission";

        types = new List<string>();
        for (int i = 0; i < Helper.items.Length; i++)
        {
            string temp = Helper.items[i];
            types.Add(temp);
        }
    }

    public override void Initalize()
    {
        for(int i = 0; i < types.Count; i++)
		{
            GameObject temp = new GameObject("Mission");
            Mission m = temp.AddComponent<Mission>();
            m.transform.parent = gameObject.transform;
            m.Setup(types[i]);
            missions.Add(m);
        }
    }

    public override void attachHosts(GameObject[] hosts)
	{
        if (Helper.SceneType() == Helper.MENU) return;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Mission temp =  missions[i];
            temp.addHost(hosts[i]);
            temp.Load();

            TextMeshProUGUI tmpro = hosts[i].GetComponentInChildren<TextMeshProUGUI>();

            tmpro.text = temp.ComponentsText;
        }
    }
}
