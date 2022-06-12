using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data: MonoBehaviour
{
    protected GameObject host;
    protected string tagName;

    public void addHost(GameObject obj)
    {
        host = obj;
        Load();
    }

    private void Awake()
    {
        Setup();
    }

    public virtual void Initalize(bool bool1, bool bool2)
    {

    }

    public void Load()
    {
        switch (Helper.SceneType())
        {
            case Helper.HIVE:
                loadHive();
                break;
            case Helper.MAP:
                loadMap();
                break;
            case Helper.PAUSE:
                loadPause();
                break;
            case Helper.SUMMARY:
                loadSummary();
                break;
        }
    }

    public virtual void Setup()
    {

    }

    public virtual void loadPause()
    {
       
    }
    public virtual void loadMap()
    {

    }
    public virtual void loadSummary()
    {

    }
    public virtual void loadHive()
    {
        Debug.Log("rgs");
    }
}
