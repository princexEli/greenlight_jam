using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Inventory_Slot : MonoBehaviour
{
    [SerializeField]
    string type;
    public bool isFull
	{
		get
		{
            return current == max;
		}
	}
    int current = 0, max=5;
    int hiveValue = 0;
    TextMeshProUGUI display;
    GameObject host;

	public void initialize(int i)
	{
        type = Helper.items[i];
    }

    public void Load()
	{
		switch (Helper.SceneType())
		{
            case Helper.HIVE:
                setupHive();
                break;
            case Helper.PAUSE:
            case Helper.MAP:
                setupPause();
                break;
            case Helper.SUMMARY:
                setupSummary();
                break;
        }
    }

    public void addHost(GameObject obj)
	{
        Debug.Log("Added host");
        host = obj;
        Load();
        updateDisplay();
	}

    TextMeshProUGUI before, after;
    private void setupSummary()
	{
        TextMeshProUGUI displayTitle = host.transform.Find("Title").gameObject.GetComponent<TextMeshProUGUI>();
        displayTitle.text = type;

        before = host.transform.Find("Ship Before").gameObject.GetComponent<TextMeshProUGUI>();
        before.text = hiveValue.ToString();

        display = host.transform.Find("Loot Info").gameObject.GetComponent<TextMeshProUGUI>();
        display.text = "+" + current;
        hiveValue += current;

        after = host.transform.Find("Ship After").gameObject.GetComponent<TextMeshProUGUI>();
        after.text = hiveValue.ToString();
    }

	#region The Hive
	public void setupHive()
    {
        TextMeshProUGUI displayTitle = host.transform.Find("Title").gameObject.GetComponent<TextMeshProUGUI>();
        displayTitle.text = type;

        display = host.transform.Find("Info").gameObject.GetComponent<TextMeshProUGUI>();
        updateDisplay();
    }
    public void addToHive()
	{

	}
    #endregion

    #region Pause
    public void setupPause()
    {
        TextMeshProUGUI displayTitle = host.transform.Find("Title").gameObject.GetComponent<TextMeshProUGUI>();
        displayTitle.text = type;

        display = host.transform.Find("Info").gameObject.GetComponent<TextMeshProUGUI>();
        updateDisplay();
    }
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
    #endregion


   
    
    

    private void updateDisplay(){
        if (Helper.SceneType() == "Hive")
		{
            display.text = hiveValue.ToString();

        }
        else if(Helper.SceneType() == Helper.SUMMARY)

        {
            display.text = "+" + current;

        }
        else
		{
            display.text = current + "/" + max;
        }
    }
}
