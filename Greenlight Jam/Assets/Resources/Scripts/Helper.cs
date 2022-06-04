using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Helper
{
    public static string[] items = { "Scrap Metal", "Electronics", "Medication", "Trinkets"};

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

    public static string SceneName()
	{
        Scene scene = SceneManager.GetActiveScene();
        return scene.name;
    }

    public static string SceneType()
    {
        if (SceneName() == "Main Menu" || SceneName() == "The Hive")
		{
            return SceneName();
		}
		else
		{
            return "Pause";
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
}
