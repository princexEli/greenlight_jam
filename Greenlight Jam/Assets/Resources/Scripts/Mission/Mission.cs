using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Mission : Data
{
	List<Mission_Component> components;
	public string ComponentsText
	{
		get
		{
			string Title = "";
			if (components.Count == 0) return "";
			foreach (Mission_Component comp in components)
			{
				string part = comp.title;
				if (Title != "")
				{
					Title += "\\n";
				}
				Title += part;
			}
			Title = Title.Replace("\\n", "\n");
			return Title;
		}
	}
	bool isRequired, isTaken;
	Button button;
	TextMeshProUGUI info;
	bool isComplete
	{
		get
		{
			foreach (Mission_Component c in components)
			{
				if (!c.isComplete)
					return false;
			}
			return true;
		}
	}

	public override void awake()
	{
		tagName = "Mission";
		components = new List<Mission_Component>();
	}

	public override void loadHive()
	{
		findChildButton();
	}
	public override void loadSummary()
	{
		findChildText();
		findChildButton();
	}
	public override void loadPause()
	{
		findChildText();
		findChildButton();
	}

	private void findChildButton()
	{
		button = host.transform.Find("Button").gameObject.GetComponent<Button>();
	}
	private void findChildText()
	{
		info = host.GetComponentInChildren<TextMeshProUGUI>();
	}

	
	

	public void loadMission()
	{
		components = new List<Mission_Component>();
		switch (Helper.SceneType())
		{
			case "Hive":
				break;
			case "Pause":
				info.text = ComponentsText;
				break;
			default:
				break;
		}
		Mission m = Game_Manager.Instance.mission.getNextMission();
		if (m == null)
		{
			host.SetActive(false);
		}
		else
		{
			components = m.components;

		}
	}

	public override void Initalize(bool isMain)
	{
		isRequired = isMain;
		isTaken = isMain;

		HashSet<string> componentItems = new HashSet<string>();
		for (int i = 0; i < Helper.randomNum(Game_Manager.Instance.maxComponents); i++)
		{
			components.Add(new Mission_Component(Helper.randomItem(componentItems), Helper.randomNum(Game_Manager.Instance.maxLootType)));
		}
	}

	public void updateButtonName()
	{
		TextMeshProUGUI tmpro = host.transform.Find("Button").gameObject.transform.GetComponentInChildren<TextMeshProUGUI>();
		if (isRequired)
		{
			tmpro.text = "Required";
			button.interactable = false;
		}
		else if (!isTaken)
		{
			tmpro.text = "Accept";
		}
		else if (!isComplete)
		{
			tmpro.text = "In Progress";
			button.interactable = false;
		}
		else
		{
			tmpro.text = "Turn In";
		}
	}

	public void onButtonClick()
	{
		if (!isTaken)
		{
			isTaken = true;
			Game_Manager.Instance.mission.addData(this);
		}
		else if (isComplete)
		{
			Game_Manager.Instance.mission.removeData(this);
		}
		updateButtonName();
	}
}
