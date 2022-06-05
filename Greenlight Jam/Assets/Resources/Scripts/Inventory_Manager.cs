using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{
    #region instance
    private static Inventory_Manager instance;
    public static Inventory_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Inventory_Manager>();
            }

            return instance;
        }
        set { instance = value; }
    }
    #endregion

    public List<Inventory_Slot> slots;

    // Start is called before the first frame update
    void Start()
    {
        if (Helper.SceneType() == "Ground")
		{
            if (slots.Count != Helper.items.Length)
            {
                Debug.LogError("Number of inventory slots (" + slots.Count + ") does not equal number of inventory types(" + Helper.items.Length + ").");
            }

            int i = 0;
            foreach (Inventory_Slot slot in slots)
            {
                slot.setupPause(Helper.items[i]);
                i++;
            }
        }   
    }

    public List<string> gainLoot(string name, int value)
	{
        List<string> temp = slots[System.Array.IndexOf(Helper.items, name)].addtoCurrent(value);
        return temp;
	}

    public void gainUnique()
	{
        //hasUnique = true;
	}
}
