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
    Upgrade_Manager upgradeM;
    Inventory_Manager inventoryM;
    Audio_Manager audioM;

    AudioSource[] sources;

	private void Awake()
	{
        setupManagers();
        DontDestroyOnLoad(this.gameObject);
    }

    private void setupManagers()
    {
        missionM = GetComponent<Mission_Manager>();
        upgradeM = GetComponent<Upgrade_Manager>();
        inventoryM = GetComponent<Inventory_Manager>();
        audioM = GetComponent<Audio_Manager>();
    }

	void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
