using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegainResources : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			GameController.instance.RegainResources();
		}
	}
}
