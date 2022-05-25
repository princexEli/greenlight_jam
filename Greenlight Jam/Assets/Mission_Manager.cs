using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_Manager : MonoBehaviour
{
    #region instance
    private static Mission_Manager instance;
    public static Mission_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Mission_Manager>();
            }

            return instance;
        }
        set { instance = value; }
    }
    #endregion

    public List<string> items;
    List<Mission> activeMissions;

    public int maxComponents = 3, maxItems = 5;

	private void Start()
	{
		if(maxComponents > items.Count)
		{
            Debug.LogError("maxComponent will be set to items.Count, since the value entered will create an infinate loop");
            maxComponents = items.Count;
		}

        activeMissions = new List<Mission>();
	}
}
