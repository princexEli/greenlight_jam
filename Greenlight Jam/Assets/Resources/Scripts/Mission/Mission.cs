﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Mission : MonoBehaviour
{
    public string title
	{
		get
		{
            string Title = "";
            if (components.Count == 0) return "";
            foreach(Mission_Component comp in components)
			{
                string part = comp.title;
                if(Title != "")
				{
                    Title += "\\n";
				}
                Title += part;
			}
            Title = Title.Replace("\\n", "\n");
            return Title;
		}
	}
    bool isRequired;
    bool isTaken;
    GameObject button;
    Locked_Panel lockPanel;
    bool isComplete
	{
		get
		{
            foreach(Mission_Component c in components)
			{
                if (!c.isComplete)
                    return false;
			}
            return true;
		}
	}
    TextMeshProUGUI info;

    List<Mission_Component> components;

	private void Awake()
	{
        components = new List<Mission_Component>();
        switch (Helper.SceneType())
		{
            case "Hive":
                button = gameObject.transform.Find("Button").gameObject;
                lockPanel = gameObject.GetComponentInChildren<Locked_Panel>();
                break;
            case "Pause":
                info = gameObject.GetComponentInChildren<TextMeshProUGUI>();
                break;
            default:
                break;
        }
    }

    private void loadMission()
	{
        components = new List<Mission_Component>();
        switch (Helper.SceneType())
        {
            case "Hive":
                break;
            case "Pause":
                loadMission();
                info.text = title;
                break;
            default:
                break;
        }
        Mission m = Mission_Manager.Instance.getNextMission();
        if(m == null)
		{
            gameObject.SetActive(false);
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
            get {

                return required + " " + item;
            }
        }
	}

	public void setupMission(bool isMain, bool isLocked)
	{
        components = new List<Mission_Component>();
        isRequired = isMain;
        isTaken = isMain;
        lockPanel.enable = isLocked;
        HashSet<string> componentItems = new HashSet<string>();
        for (int i = 0; i < Helper.randomNum(Mission_Manager.Instance.maxComponents); i++) 
        {
            components.Add(new Mission_Component(Helper.randomItem(componentItems), Helper.randomNum(Mission_Manager.Instance.maxItems)));
        }

        updateButton();
    }

    public void updateButton()
	{
        Button button = this.button.GetComponent<Button>();
        TextMeshProUGUI tmpro = this.button.GetComponentInChildren<TextMeshProUGUI>();
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
            Mission_Manager.Instance.addMission(this);
        }
        else if (isComplete)
		{
            Mission_Manager.Instance.removeMission(this);
        }
        updateButton();
	}
}
