using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTheLoot : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "player")
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				GameController.instance.KeepTheLoot();
			}
		}
	}
}
