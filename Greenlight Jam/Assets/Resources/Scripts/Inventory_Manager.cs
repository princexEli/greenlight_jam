﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Manager : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        if (Helper.SceneType() == "Pause")
		{
            slots = new List<Inventory_Slot>();
            int i = 0;
            GameObject[] slotObj = GameObject.FindGameObjectsWithTag("Inventory");
            if (slotObj.Length != Helper.items.Length)
            {
                Debug.LogError("Number of inventory slots (" + slots.Count + ") does not equal number of inventory types(" + Helper.items.Length + ").");
            }
            foreach (GameObject slot in slotObj)
            {
                Inventory_Slot temp = slot.GetComponent<Inventory_Slot>();
                temp.setupPause(Helper.items[i]);
                slots.Add(temp);
                i++;
            }
        }   
    }

    public List<string> gainLoot(string name, int value)
	{
        List<string> temp = slots[System.Array.IndexOf(Helper.items, name)].addtoCurrent(value);
        return temp;
	}

    public void gainUnique()
	{
        //hasUnique = true;
	}
}
