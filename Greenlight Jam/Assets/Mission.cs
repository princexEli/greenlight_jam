using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    string title = "Mission", giver = "Captain Ash";
    bool isRequired;
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
    Mission_Manager manager;
    List<Mission_Component> components;

	private void Start()
	{
        manager = Mission_Manager.Instance;
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
	}

	public Mission(bool isMain)
	{
        components = new List<Mission_Component>();
        isRequired = isMain;
        HashSet<string> componentItems = new HashSet<string>();

        for (int i = 0; i < randomNum(manager.maxComponents); i++) 
        {
            components.Add(new Mission_Component(randomItem(componentItems), randomNum(manager.maxItems)));
        }
    }

    private string randomItem(HashSet<string> itemsToExclude)
    {
        string item;
        do {
            item = manager.items[Random.Range(1, manager.items.Count - 1)];
        } while (!itemsToExclude.Contains(item));
        itemsToExclude.Add(item);
        return item;
    }

    private static int randomNum(int max)
	{
        return Random.Range(1, max);
    }
}
