using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory_Slot : MonoBehaviour
{
    string type;
    public bool isFull
	{
		get
		{
            return current == max;
		}
	}
    int current = 0, max=5;
    TextMeshProUGUI display;
    public List<string> addtoCurrent(int value)
	{
        List<string> temp = new List<string>();
        if (current == max) return temp;

        int added = value;
        int result = current + value;
        if (result > max) 
        { 
            int remainder = result - max;
            added = value - remainder;
            result = max;
		}
        temp.Add("+ " + added + " " + type);

        if (result == max)
        {
            temp.Add(type + " inventory now full");
        }

        current = result;
        updateDisplay();

        return temp;
	}

    public void setupPause(string type)
	{
        this.type = type;
        TextMeshProUGUI displayTitle = gameObject.transform.Find("Title").gameObject.GetComponent<TextMeshProUGUI>();
        displayTitle.text = type;

        display = gameObject.transform.Find("Info").gameObject.GetComponent<TextMeshProUGUI>();
        updateDisplay();
    }
    
    private void updateDisplay(){
        display.text = current + "/" + max;
    }
}
