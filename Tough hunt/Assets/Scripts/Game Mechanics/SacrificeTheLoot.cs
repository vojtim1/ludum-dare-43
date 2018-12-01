using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeTheLoot : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				GameController.instance.SacrificeTheLoot();
			}
		}
	}
}
