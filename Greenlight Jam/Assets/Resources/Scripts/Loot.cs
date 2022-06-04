using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using System;

public class Loot : MonoBehaviour
{
	[Header("Is Special")]
	public bool isUnique = false;
	
	[Header("Possible Loot")]
	public int minItems = 0, maxItems = 5;
	public bool metal, electronics, meds, trinkets;
	Outline highlight;

	List<string> loot;
	bool isLooted = false;

    private void Awake()
	{
		highlight = gameObject.AddComponent<Outline>();
		highlight.color = 1;
	}

	private void Start()
	{
		randomizeLoot();
		highlight.enabled = false;
	}

	private void randomizeLoot()
	{

	}

	public void interact()
	{
		if (isLooted) return;

		isLooted = true;
		tag = "Empty";
	}

	internal void Highlight(bool isActive)
	{
		highlight.enabled = isActive;
	}
}
