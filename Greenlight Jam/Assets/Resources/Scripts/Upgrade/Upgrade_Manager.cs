using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Manager : Data_Manager
{
    #region instance
    private static Upgrade_Manager instance;
    public static Upgrade_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Upgrade_Manager>();
            }

            return instance;
        }
        set { instance = value; }
    }
    #endregion

    public List<string> items;
    List<Upgrade> availibleUpgrades, purchasedUpgrades;
    GameObject[] upgradeUI;
    List<string> types;
    List<Upgrade> upgrades;

    public override void awake()
    {
        types = new List<string>();
        upgrades = new List<Upgrade>();
        tagName = "Upgrade";
        types.Add("Timer");

        for(int i = 0; i < Helper.items.Length; i++)
		{
            string temp = Helper.items[i];
            types.Add(temp);
		}
    }

    public void LoadHive()
    {
        availibleUpgrades = new List<Upgrade>();
        purchasedUpgrades = new List<Upgrade>();
        upgradeUI = GameObject.FindGameObjectsWithTag("Upgrade");

        for (int i = 0; i < upgradeUI.Length; i++)
        {
            Upgrade temp = upgradeUI[i].GetComponent<Upgrade>();
            availibleUpgrades.Add(temp);
        }
    }

	public override void Initalize()
	{
        for (int i = 0; i < types.Count; i++)
        {
            GameObject temp = new GameObject("Upgrade Slot");
            temp.transform.parent = gameObject.transform;
            Upgrade slot = temp.AddComponent<Upgrade>();
            slot.initialize(types[i]);
            upgrades.Add(slot);
        }
    }

	internal void UpdateButtons()
	{
		foreach(Upgrade g in upgrades)
		{
            g.updateButton();
		}
	}

	public override void attachHosts(GameObject[] hosts)
    {
        if (Helper.SceneType() != Helper.HIVE) return;
        if (hosts.Length == 0) Debug.LogError("No hosts found for upgrade slots.");
        for (int i = 0; i < upgrades.Count; i++)
        {
            upgrades[i].addHost(hosts[i]);
            upgrades[i].Load();
        }
    }

	public int max(string type)
	{
        if (type == "Upgrade Points: ") return -1;
        int pos = types.IndexOf(type);
        Upgrade u = upgrades[pos];
        return Game_Manager.Instance.startingInventorySize + u.level * 5;
	}
}
