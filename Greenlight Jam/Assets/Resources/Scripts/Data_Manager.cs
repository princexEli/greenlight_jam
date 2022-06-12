using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Manager : MonoBehaviour
{
    protected GameObject[] hosts;
    protected string tagName;
    int unlocked = 1;
    int maxData = 4;

    private void Awake()
    {
        Setup();
    }

    public void Load()
    {
        hosts = GameObject.FindGameObjectsWithTag(tagName);
        attachHosts(hosts);
    }
    public virtual void Setup() { }
    public virtual void Initalize() { }
    public virtual void attachHosts(GameObject[] hosts) { }
    public virtual void addData(Data d) { }
    public virtual void removeData(Data d) { }
}
