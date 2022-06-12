using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Update_Panel : MonoBehaviour
{
    #region instance
    private static Update_Panel instance;
    public static Update_Panel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Update_Panel>();
            }

            return instance;
        }
        set { instance = value; }
    }
    #endregion

    public GameObject linePrefab;
    public int maxLines = 10;
    public float maxTime = 5f, fadeTime =1f;
    TextMeshProUGUI tmp;
    float lastUpdate =-1;
    Color origColor;

    void Awake()
	{
        tmp = GetComponent<TextMeshProUGUI>();
        origColor = tmp.color;
	}
    void Update()
	{
        if (tmp.text != "")
        {
            if (Time.time - lastUpdate >= maxTime)
            {
                StartCoroutine(FadeTextToZeroAlpha());
            }
        }
	}

    public IEnumerator FadeTextToZeroAlpha()
    {
        while (tmp.color.a > 0.0f)
        {
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, tmp.color.a - (Time.deltaTime / fadeTime));
            yield return null;
        }
    }


    private void removeLine()
	{
        tmp.text = "";
    }

    public void addLines(List<string> values)
	{
        string temp = "";

        for(int tmp=0; tmp<values.Count;tmp++)
		{
            if (tmp != 0)
			{
                temp = temp + "\\n";
            }
            temp = temp + values[tmp];
            
		}
        temp = temp.Replace("\\n", "\n");
        tmp.color = origColor;
        tmp.text = temp;
        lastUpdate = Time.time;
    }

    public void addLine(string value)
	{
        List<string> temp = new List<string>();
        temp.Add(value);
        addLines(temp);
	}
}
