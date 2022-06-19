using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Mission : Data
{
	public string ComponentsText
	{
		get
		{
			return "Collect " + needed + " " + type;
		}
	}
	string type;
	Button button;
	Inventory_Slot slot;
	int needed = 1;

	TextMeshProUGUI info;
	public override void awake()
	{
		tagName = "Mission";
	}

	public void Setup(string name)
	{
		type = name;
	}

	public override void loadHive()
	{
		if(slot ==null) slot = Game_Manager.Instance.inventory.getLoot(type);
		button = host.transform.Find("Button").gameObject.GetComponent<Button>();
		button.onClick.AddListener(delegate () { onButtonClick(); });
		updateButtonName();
	}

	public override void loadSummary()
	{
		loadHive();
	}

	private void hiveButtons(TextMeshProUGUI tmpro)
	{
		tmpro.text = slot.hiveValue + "/" + needed;
		if (slot.hiveValue < needed)
		{
			button.interactable = false;
		}
		else
		{
			button.interactable = true;
		}
			
	}

	public void updateButtonName()
	{
		TextMeshProUGUI tmpro =button.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>();

		string type = Helper.SceneType();
		if (type == Helper.HIVE)
		{
			hiveButtons(tmpro);
		}
	}

	public void onButtonClick()
	{
		Game_Manager.Instance.inventory.points++;
		finishGoal();
		hiveButtons(button.GetComponentInChildren<TextMeshProUGUI>());
		Game_Manager.Instance.upgrade.UpdateButtons();
	}

	private void finishGoal()
	{
		if (slot.hiveValue > needed)
			slot.hiveValue -= needed;
		setGoal();
	}

	public void setGoal()
	{
		if(needed == 1)
		{
			needed = 5;
		}
		else if (needed < 50)
		{
			needed += 10;
		}
		else if (needed < 100)
		{
			needed += 50;
		}
		else if (needed < 500)
		{
			needed += 1000;
		}
	}
}
