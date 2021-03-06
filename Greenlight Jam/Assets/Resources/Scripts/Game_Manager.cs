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

    public Material skybox;
    public float timeLimit = 10f;
    public int countdown = 3;
    public AudioClip timerSound;
    public AudioClip countDownSound;
    public int levelMultiplier = 1;

    #region Loot
    [Header("Loot")]
    public int minLoot = 1;
    public AudioClip lootSound;
    GameObject lootable, looted;
	#endregion

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
    public int unlockedMissions = 1;
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
    public int unlockedUpgrades = 0;
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
        if (Game_Manager.Instance != this) Destroy(gameObject);
        setupManagers();
        DontDestroyOnLoad(this.gameObject);
    }

    private void setupManagers()
    {
        if (missionM != null) return;
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

    private void createLoot()
	{
        foreach (Transform child in lootable.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in looted.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void Load()
	{
        RenderSettings.skybox = skybox;
        DynamicGI.UpdateEnvironment();
        audioM.Load();
        inventoryM.BeginLoad();
        missionM.BeginLoad();
        upgradeM.BeginLoad();
    }
}
