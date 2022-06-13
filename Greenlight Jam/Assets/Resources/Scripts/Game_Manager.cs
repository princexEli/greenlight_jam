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
    
    Mission_Manager missionM;
    public Mission_Manager mission
	{
		get
		{
            return missionM;
		}
	}
    [Header("Mission Manager")]
    public string[] lootTypes = { "Scrap Metal", "Med(s)", "Electronic(s)", "Trinket(s)" };
    
    Upgrade_Manager upgradeM;
    public Upgrade_Manager upgrade
    {
        get
        {
            return upgradeM;
        }
    }
    [Header("Upgrade Manager")]

    Inventory_Manager inventoryM;
    public Inventory_Manager inventory
    {
        get
        {
            return inventoryM;
        }
    }
    [Header("Inventory Manager")]

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

    private void Awake()
	{
        setupManagers();
        DontDestroyOnLoad(this.gameObject);
    }

    private void setupManagers()
    {
        missionM = gameObject.AddComponent<Mission_Manager>();
        upgradeM = gameObject.AddComponent<Upgrade_Manager>();
        inventoryM = gameObject.AddComponent<Inventory_Manager>();
        audioM = gameObject.AddComponent<Audio_Manager>();
    }

    public void LoadScene()
	{
        audioM.swapTheme();
        missionM.Load();
        upgradeM.LoadHive();
        inventoryM.Load();
	}

    public void updateVolume(float val)
	{
        audioM.updateVolume(val);
	}
}
