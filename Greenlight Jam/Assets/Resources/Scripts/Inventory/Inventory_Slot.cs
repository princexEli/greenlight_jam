using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Inventory_Slot : Data
{
    [SerializeField]
    public string type;
    public bool isFull
	{
		get
		{
            return current == max;
		}
	}
    [SerializeField]
    public int current = 0, loot=0;
    public int hiveValue
    {
		get
		{
            return current;
		}
		set
		{
            current = value;
            updateDisplay();
		}
	}
    int max;
    TextMeshProUGUI display, before, after;

	public override void awake()
	{
        tagName = "Inventory";
        max = Game_Manager.Instance.startingInventorySize;
	}

	public void initialize(int i)
	{
        if(i != -1)
		{
            type = Helper.items[i];
        }
		else
		{
            type = "Upgrade Points: ";
		}
        
        gameObject.name = type + " Slot";
    }

	public override void loadSummary()
	{
        hiveValue += loot;
        loot = 0;
    }

	public override void loadHive()
	{
        TextMeshProUGUI displayTitle = host.transform.Find("Title").gameObject.GetComponent<TextMeshProUGUI>();
        displayTitle.text = type;

        display = host.transform.Find("Info").gameObject.GetComponent<TextMeshProUGUI>();
        updateDisplay();
    }

    public override void loadPause()
	{
        if (type == "Upgrade Points: ") return;
        max = Game_Manager.Instance.upgrade.max(type);
        TextMeshProUGUI displayTitle = host.transform.Find("Title").gameObject.GetComponent<TextMeshProUGUI>();
        displayTitle.text = type;

        display = host.transform.Find("Info").gameObject.GetComponent<TextMeshProUGUI>();
        updateDisplay();
    }

    public List<string> AddToCurrent(int value)
    {
        List<string> temp = new List<string>();
        if (loot == max) return temp;

        int added = value;
        int result = loot + value;
        if (result > max)
        {
            int remainder = result - max;
            added = value - remainder;
            result = max;
        }
        temp.Add("+ " + added + " " + type);

        if (result == max)
        {
            temp.Add(type + " inventory now full");
        }

        loot = result;
        updateDisplay();

        return temp;
    }

    public void updateDisplay(){
        if (Helper.SceneType() == "Hive")
		{
            display.text = hiveValue.ToString();
        }
        else if(Helper.SceneType() == Helper.SUMMARY)
        {
            display.text = "+" + current;

        }
        else
		{
            display.text = loot + "/" + max;
        }
    }
}
