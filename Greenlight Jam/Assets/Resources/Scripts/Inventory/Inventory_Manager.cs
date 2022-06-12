using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Manager : Data_Manager
{
	#region instance
	private static Inventory_Manager instance;
	public static Inventory_Manager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<Inventory_Manager>();
			}

			return instance;
		}
		set { instance = value; }
	}
	#endregion

	List<Inventory_Slot> slots;
	public override void Setup()
	{
		slots = new List<Inventory_Slot>();
		tagName = "Inventory";
		Initalize();
	}
	public override void Initalize()
	{
		for (int i = 0; i < Helper.items.Length; i++)
		{
			Inventory_Slot temp = gameObject.AddComponent<Inventory_Slot>();
			temp.initialize(i);
			slots.Add(temp);
		}
	}
	public override void attachHosts(GameObject[] hosts)
	{
		for(int i = 0; i < slots.Count; i++)
		{
			slots[i].addHost(hosts[i]);
			slots[i].Load();
		}
	}
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
