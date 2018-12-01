using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTheLoot : MonoBehaviour {

	private void OnTriggerStay2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				GameController.instance.KeepTheLoot();
			}
		}
	}
}
