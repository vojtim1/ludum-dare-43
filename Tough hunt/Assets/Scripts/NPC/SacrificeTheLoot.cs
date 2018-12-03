using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeTheLoot : NPC {

	// TODO: merge all similar classes into one and make these children of it
	[SerializeField]
	private float secondsBetweenInteractions;
	private float lastInteractionTime;
	protected override void Interact()
	{
		lastInteractionTime = Time.time;
		GameController.instance.SacrificeTheLoot();
	}
}
