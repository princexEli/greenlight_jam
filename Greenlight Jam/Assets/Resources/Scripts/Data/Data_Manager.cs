using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Manager : MonoBehaviour
{
    protected GameObject[] hosts;
    protected string tagName;
    int maxData = 4;

    private void Awake()
    {
        awake();
        Initalize();
    }
    public virtual void awake() { }

    public void BeginLoad()
    {
        hosts = GameObject.FindGameObjectsWithTag(tagName);
        attachHosts(hosts);
        Load();
    }
    public virtual void Load() { }
    
    public virtual void Initalize() { }
    public virtual void attachHosts(GameObject[] hosts) { }
    public virtual void addData(Data d) { }
    public virtual void removeData(Data d) { }
}
