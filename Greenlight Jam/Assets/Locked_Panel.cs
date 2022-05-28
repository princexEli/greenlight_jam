using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked_Panel : MonoBehaviour
{
	public bool enable 
	{ 
		set 
		{
			if (value)
			{
				gameObject.SetActive(true);
			}
			else
			{
				gameObject.SetActive(false);
			}
		} 
	}
}
