using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Manager : MonoBehaviour
{
    #region instance
    private static Upgrade_Manager instance;
    public static Upgrade_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Upgrade_Manager>();
            }

            return instance;
        }
        set { instance = value; }
    }
    #endregion

    public List<string> items;
    int unlockedUpgradeSlots = 1;
    List<Upgrade> availibleUpgrades, purchasedUpgrades;
    GameObject[] upgradeUI;

    public void LoadHive()
    {
        availibleUpgrades = new List<Upgrade>();
        purchasedUpgrades = new List<Upgrade>();
        upgradeUI = GameObject.FindGameObjectsWithTag("Upgrade");

        for (int i = 0; i < upgradeUI.Length; i++)
        {
            Upgrade temp = upgradeUI[i].GetComponent<Upgrade>();
            temp.setupUpgrade(i >= unlockedUpgradeSlots);
            availibleUpgrades.Add(temp);
        }
    }

    public void LoadPause()
	{

	}
}
