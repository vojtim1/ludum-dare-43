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
				if(GameController.instance.CurrentGameTime > GameController.instance.TotalDayTime / 2)
				{
					GameController.instance.CurrentGameTime = 0;
				}
			}
		}
	}
}
