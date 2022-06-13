using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Mission : Data
{
	Mission_Manager manager;
	private void Start()
	{
		manager = Game_Manager.Instance.mission;
	}
	public string title
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
	[SerializeField]
	bool isRequired, isTaken;
	Button button;
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
	TextMeshProUGUI info;

	List<Mission_Component> components;

	public override void loadHive()
	{
		Debug.Log("loadHive");
		button = host.transform.Find("Button").gameObject.GetComponent<Button>();
		Debug.Log("Button");
	}

	public override void loadPause()
	{
		info = host.GetComponentInChildren<TextMeshProUGUI>();
	}
	public override void Setup()
	{
		tagName = "Mission";
		components = new List<Mission_Component>();
	}

	public void loadMission()
	{
		components = new List<Mission_Component>();
		switch (Helper.SceneType())
		{
			case "Hive":
				break;
			case "Pause":
				info.text = title;
				break;
			default:
				break;
		}
		Mission m = manager.getNextMission();
		if (m == null)
		{
			host.SetActive(false);
		}
		else
		{
			components = m.components;

		}
	}

	private class Mission_Component
	{
		string item;
		int current = 0, required;
		public bool isComplete
		{
			get
			{
				return current >= required;
			}
		}

		public Mission_Component(string itemName, int requiredAmount)
		{
			required = requiredAmount;
			item = itemName;
		}

		public string title
		{
			get
			{

				return required + " " + item;
			}
		}
	}

	public override void Initalize(bool isMain, bool bool2)
	{
		isRequired = isMain;
		isTaken = isMain;

		HashSet<string> componentItems = new HashSet<string>();
		for (int i = 0; i < Helper.randomNum(manager.maxComponents); i++)
		{
			components.Add(new Mission_Component(Helper.randomItem(componentItems), Helper.randomNum(manager.maxItems)));
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
			manager.addData(this);
		}
		else if (isComplete)
		{
			manager.removeData(this);
		}
		updateButtonName();
	}
}
