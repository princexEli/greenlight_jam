using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using System;

public class Loot : MonoBehaviour
{
	[Header("Is Special")]
	[Tooltip("Is true, there will be no random loot.")]
	public bool isUnique = false;
	
	[Header("Possible Loot")]
	public int minItems = 0, maxItems = 5;
	Outline highlight;

	bool isLooted = false;
	Dictionary<string, int> loot;

	private void Awake()
	{
		highlight = gameObject.AddComponent<Outline>();
		highlight.color = 1;
		tag = "Lootable";
		gameObject.layer = LayerMask.NameToLayer("Lootable");
		loot = new Dictionary<string, int>();
	}

	private void Start()
	{
		highlight.enabled = false;
	}

	public void obtainLoot()
	{
		randomizeLoot();
		List<string> temp = new List<string>();

		if (isUnique)
		{
			//Inventory_Manager.Instance.gainUnique();
		}
		else
		{
			foreach (string s in loot.Keys)
			{
				temp.AddRange(Inventory_Manager.Instance.gainLoot(s, loot[s]));
			}
		}

		Update_Panel.Instance.addLines(temp);
	}

	private void randomizeLoot()
	{
		if (isUnique) return;

		for (int i = 0; i < Helper.randomNum(minItems, maxItems); i++)
		{
			string temp = Helper.randomItem();
			if (loot.ContainsKey(temp))
			{
				loot[temp]++;
			}
			else
			{
				loot.Add(temp, 1);
			}
		}

		if (loot.Count == 0)
		{
			Update_Panel.Instance.addLine("Container empty");
		}
	}

	public void interact()
	{
		if (isLooted) return;
		obtainLoot();
		isLooted = true;
		tag = "Empty";
		Character_Manager.Instance.cancelHighlight();
	}

	internal void Highlight(bool isActive)
	{
		try
		{
			highlight.enabled = isActive;
		}
		catch (Exception ex)
		{
			Debug.LogError("Failure to set highlight to "+isActive +". "  + ex);
		}
	}
}
