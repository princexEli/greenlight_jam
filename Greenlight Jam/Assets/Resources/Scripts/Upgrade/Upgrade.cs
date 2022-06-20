using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : Data
{
	string type;
	public string label
	{
		get
		{
			return type +": Level "+level;
		}
	}
	public string buttonCost
	{
		get
		{
			return "Cost: " + cost;
		}
	}
	public int level = 0;
	public int Level 
	{ 
		get { return level; } 
		set
		{
			level = value;
			if(displayTitle != null)
			{
				displayTitle.text = label;
			}
		} 
	}
	Button button;
	public int cost
	{
		get
		{
			return (level / 5) + 1;
		}
	}
	public void initialize(string name)
	{
		type = name;
	}
	TextMeshProUGUI displayTitle, costDisplay;
	public override void loadHive()
	{
		if (points == null)
			points = Game_Manager.Instance.inventory.upgradePoints;
		displayTitle = host.transform.Find("Info").gameObject.GetComponent<TextMeshProUGUI>();
		displayTitle.text = label;
		
		button = host.GetComponentInChildren<Button>();
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(delegate () { onButtonClick(); });
		costDisplay = button.GetComponentInChildren<TextMeshProUGUI>();
		updateButton();
	}

	public void updateCostDisplay()
	{
		costDisplay.text = buttonCost;
	}

	Inventory_Slot points;
	public void onButtonClick()
	{
		points.hiveValue -= cost;
		Level++;
		updateButton();
	}

	internal void updateButton()
	{
		updateCostDisplay();
		if (Game_Manager.Instance.inventory.points < cost)
		{
			button.interactable = false;
		}
		else
		{
			button.interactable = true;
		}
	}
}
