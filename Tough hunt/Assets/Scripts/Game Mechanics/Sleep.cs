using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour {
	private void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			if (Input.GetKey(KeyCode.E))
			{
				GameController gc = GameController.instance;
				if(gc.CurrentGameTime > gc.TotalDayTime / 2)
				gc.CurrentGameTime = 0;
			}
		}
	}
}
