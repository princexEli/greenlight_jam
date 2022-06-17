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

	public override void Initalize(bool isMain)
	{
		if (components == null) awake();
		isRequired = isMain;
		isTaken = isMain;

		HashSet<string> componentItems = new HashSet<string>();
		for (int i = 0; i < Helper.randomNum(Game_Manager.Instance.maxComponents); i++)
		{
			string item = Helper.randomItem(componentItems);
			int amount = Helper.randomNum(Game_Manager.Instance.maxLootType);
			Mission_Component c = new Mission_Component(item, amount);
			components.Add(c);
		}
	}

	public override void loadHive()
	{
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

	private void summaryButtons(TextMeshProUGUI tmpro)
	{
		if (!isComplete)
		{
			tmpro.text = "In Progress";
			button.interactable = false;
		}
		else
		{
			tmpro.text = "Turn In";
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
		else if(type == Helper.SUMMARY)
		{
			summaryButtons(tmpro);
		}
	}

	public void onButtonClick()
	{
		if (!isTaken)
		{
			isTaken = true;
			transform.parent = Game_Manager.Instance.mission.activeMissions.transform;
			Game_Manager.Instance.mission.addData(this);
		}
		else if (isComplete)
		{
			Game_Manager.Instance.mission.removeData(this);
		}
		updateButtonName();
	}
}
