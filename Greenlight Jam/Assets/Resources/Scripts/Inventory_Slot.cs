using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory_Slot : MonoBehaviour
{
    string type;
    int Current = 0, max=5;
    TextMeshProUGUI display;
    public int current
    {
        get
        {
            return Current;
        }
        set
        {
            int added = 0;
            for (int i = 0; i < value; i++)
            {
                if (Current + 1 <= max)
                {
                    Current++;
                    added++;
                }
                else
                {
                    Debug.Log("Inventory full.");
                    break;
                }
            }

            if (added != 0)
            {
                Debug.Log("+ " + added + " " + type);
            }

            updateDisplay();
        }
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
