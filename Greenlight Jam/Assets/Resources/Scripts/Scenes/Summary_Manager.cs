using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summary_Manager : MonoBehaviour
{
    void Start()
    {
        if(Inventory_Manager.Instance == null)
		{
            Helper.setupTest();
		}
        Inventory_Manager.Instance.Load();
    }

    public void OnClickContinue()
    {
        Helper.changeScene(Helper.HIVE);
    }
}
