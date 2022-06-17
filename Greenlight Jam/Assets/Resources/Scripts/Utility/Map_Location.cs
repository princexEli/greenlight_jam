using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map_Location : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		loadLocation();
	}

	private void loadLocation()
	{
		SceneManager.LoadScene(gameObject.name);
	}
}
