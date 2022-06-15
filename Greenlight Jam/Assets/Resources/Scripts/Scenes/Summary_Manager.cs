using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summary_Manager : MonoBehaviour
{
    void Start()
    {
        //Inventory_Manager.Instance.BeginLoad();
    }

    public void OnClickContinue()
    {
        Helper.changeScene(Helper.HIVE);
    }
}
