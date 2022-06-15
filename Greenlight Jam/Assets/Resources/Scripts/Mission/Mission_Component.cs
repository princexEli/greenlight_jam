using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_Component
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
