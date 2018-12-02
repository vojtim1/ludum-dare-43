using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeTheLoot : MonoBehaviour {

	// TODO: merge all similar classes into one and make these children of it
	[SerializeField]
	private float secondsBetweenInteractions;
	private float lastInteractionTime;
	private void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.E) && (Time.time - lastInteractionTime >= secondsBetweenInteractions))
			{
				lastInteractionTime = Time.time;
				GameController.instance.SacrificeTheLoot();
			}
		}
	}
}
