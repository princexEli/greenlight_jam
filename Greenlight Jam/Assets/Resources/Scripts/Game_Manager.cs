using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Game_Manager : MonoBehaviour
{
    #region instance
    private static Game_Manager instance;
    public static Game_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Game_Manager>();
                if(instance == null)
				{
                    Helper.setupTest();
				}
            }

            return instance;
        }
        set { instance = value; }
    }
    #endregion

    public int minLoot = 1;

	#region Mission Manager
	Mission_Manager missionM;
    public Mission_Manager mission
	{
		get
		{
            return missionM;
		}
	}
    [Header("Mission Manager")]
    public int maxComponents = 3;
    public int maxLootType = 5;
    public string[] lootTypes = { "Scrap Metal", "Med(s)", "Electronic(s)", "Trinket(s)" };
	#endregion

	#region Upgrade Manager
	Upgrade_Manager upgradeM;
    public Upgrade_Manager upgrade
    {
        get
        {
            return upgradeM;
        }
    }
    [Header("Upgrade Manager")]
    #endregion

    #region Inventory Manager
    Inventory_Manager inventoryM;
    public Inventory_Manager inventory
    {
        get
        {
            return inventoryM;
        }
    }
    [Header("Inventory Manager")]
    public int startingInventorySize = 5;
    #endregion

    #region Audio Manager
    Audio_Manager audioM;
    public new Audio_Manager audio
    {
        get
        {
            return audioM;
        }
    }
    [Header("Audio Manager")]
    public float transitionDuration;
    #endregion

    private void Awake()
	{
        setupManagers();
        DontDestroyOnLoad(this.gameObject);
    }

    private void setupManagers()
    {
        GameObject temp = new GameObject("Mission Manager");
        temp.transform.parent = gameObject.transform;
        missionM = temp.AddComponent<Mission_Manager>();
        
        temp = new GameObject("Upgrade Manager");
        temp.transform.parent = gameObject.transform;
        upgradeM = temp.AddComponent<Upgrade_Manager>();

        temp = new GameObject("Inventory Manager");
        temp.transform.parent = gameObject.transform;
        inventoryM = temp.AddComponent<Inventory_Manager>();

        temp = new GameObject("Audio Manager");
        temp.transform.parent = gameObject.transform;
        audioM = temp.AddComponent<Audio_Manager>();
    }

    public void LoadScene()
	{
        audioM.swapTheme();
        inventoryM.BeginLoad();
        missionM.BeginLoad();
        upgradeM.LoadHive();
	}

    public void updateVolume(float val)
	{
        audioM.updateVolume(val);
	}
}
