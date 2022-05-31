using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Helper
{
    public static int randomNum(int max)
    {
        return Random.Range(1, max);
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
}
