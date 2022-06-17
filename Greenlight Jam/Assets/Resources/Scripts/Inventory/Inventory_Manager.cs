using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Manager : Data_Manager
{
	List<Inventory_Slot> slots;
	public override void awake()
	{
		slots = new List<Inventory_Slot>();
		tagName = "Inventory";
	}

	//Called once on awake
	public override void Initalize()
	{
		for (int i = 0; i < Helper.items.Length; i++)
		{
			GameObject temp = new GameObject("Inventory Slot");
			temp.transform.parent = gameObject.transform;
			Inventory_Slot slot = temp.AddComponent<Inventory_Slot>();
			slot.initialize(i);
			slots.Add(slot);
		}
	}

	//Called on Scene Change
	public override void attachHosts(GameObject[] hosts)
	{
		if (Helper.SceneType() == Helper.MENU) return;
		if(hosts.Length == 0) Debug.LogError("No hosts found for inventory slots.");
		for (int i = 0; i < slots.Count; i++)
		{
			slots[i].addHost(hosts[i]);
			slots[i].Load();
		}
	}

	//For when player picks up loot
	public List<string> gainLoot(string name, int value)
	{
        int pos = System.Array.IndexOf(Helper.items, name);
        List<string> temp = slots[pos].AddToCurrent(value);
        return temp;
	}
	
	public override void addData(Data d)
	{
		slots.Add((Inventory_Slot)d);
	}
	public override void removeData(Data d)
	{
		slots.Remove((Inventory_Slot)d);
	}
}
