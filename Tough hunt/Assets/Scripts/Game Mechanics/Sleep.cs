using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour {
	public float timeSpeedBoost;
	private void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.E) && GameController.instance.CurrentGameTime > (GameController.instance.TotalDayTime / 2))
			{
				GameController.instance.TimeSpeed = timeSpeedBoost;
			}
			if (Input.GetKeyUp(KeyCode.E) || GameController.instance.CurrentGameTime < (GameController.instance.TotalDayTime / 2))
			{
				GameController.instance.TimeSpeed = 1;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		GameController.instance.TimeSpeed = 1;
	}
}
