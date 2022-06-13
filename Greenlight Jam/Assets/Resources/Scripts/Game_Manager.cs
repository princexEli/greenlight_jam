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
            }

            return instance;
        }
        set { instance = value; }
    }
    #endregion
    
    Mission_Manager missionM;
    [Header("Mission Manager")]
    public string[] lootTypes = { "Scrap Metal", "Med(s)", "Electronic(s)", "Trinket(s)" };
    
    Upgrade_Manager upgradeM;
    [Header("Upgrade Manager")]

    Inventory_Manager inventoryM;
    [Header("Inventory Manager")]

    Audio_Manager audioM;
    [Header("Audio Manager")]
    public float transitionDuration;
    public AudioClip indoor_theme;
    public AudioClip hive_theme, map_theme, menu_theme;


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

    private void getThemes()
	{
        hive_theme = Resources.Load("Sound/Hive_Theme") as AudioClip;
        map_theme = Resources.Load("Sound/Map_Theme") as AudioClip;
        menu_theme = Resources.Load("Sound/Menu_Theme") as AudioClip;
    }
}
