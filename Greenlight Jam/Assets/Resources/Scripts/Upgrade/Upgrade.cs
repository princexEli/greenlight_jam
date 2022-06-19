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
			return "+5 to "+type;
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
	Button button;
	public int cost
	{
		get
		{
			if (level == 0) return 1;
			return level / 5;
		}
	}
	public void initialize(string name)
	{
		type = name;
	}

	public override void loadHive()
	{
		TextMeshProUGUI displayTitle = host.transform.Find("Info").gameObject.GetComponent<TextMeshProUGUI>();
		displayTitle.text = label;
		
		button = host.GetComponentInChildren<Button>();
		button.onClick.AddListener(delegate () { onButtonClick(); });
		TextMeshProUGUI costDislay = button.GetComponentInChildren<TextMeshProUGUI>();
		costDislay.text = buttonCost;
		updateButton();
	}

	Inventory_Slot points;
	public void onButtonClick()
	{
		if (points == null) points = Game_Manager.Instance.inventory.upgradePoints;
		points.hiveValue -= cost;
		level++;
		updateButton();
	}

	internal void updateButton()
	{
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
