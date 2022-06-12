using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Helper
{
    public static string[] items = { "Scrap Metal", "Electronic(s)", "Medication(s)", "Trinket(s)"};

    public static int randomNum(int max)
    {
        return randomNum(1, max);
    }

    public static int randomNum(int min, int max)
    {
        return Random.Range(min, max);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }

    public const string MENU = "Menu", HIVE = "Hive", SUMMARY = "Summary", MAP = "Map", PAUSE="Pause";
    public static string SceneType()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "Main Menu":
                return MENU;
            case "The Hive":
                return HIVE;
            case "Mission End":
                return SUMMARY;
            case "Ground":
                return MAP;
            default: 
                return PAUSE;
		}
	}

    public static void changeScene(string name)
	{
        switch (name)
        {
            case MENU:
                SceneManager.LoadScene("Main Menu");
                break;
            case HIVE:
                SceneManager.LoadScene("The Hive");
                break;
            case SUMMARY:
                SceneManager.LoadScene("Mission End");
                break;
            case MAP:
                SceneManager.LoadScene("Ground");
                break;
            default:
                SceneManager.LoadScene(name);
                break;
        }
    }

    public static string randomItem()
    {
        return randomItem(new HashSet<string>());
    }

    public static string randomItem(HashSet<string> itemsToExclude)
    {
        string item;
        int i = 0;
        do
        {
            item = items[Random.Range(0, items.Length)];
            i++;
        } while (itemsToExclude.Contains(item) && i < 10);
        itemsToExclude.Add(item);
        return item;
    }

    public static void setupTest()
	{
		GameObject doNotDestroy = Resources.Load("Prefabs/DontDestroyOnLoad") as GameObject;
        GameObject.Instantiate(doNotDestroy,Vector3.zero, new Quaternion());
    }
}
