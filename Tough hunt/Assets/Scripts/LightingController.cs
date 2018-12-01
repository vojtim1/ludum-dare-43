using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingController : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		float currentGameTime = GameController.instance.CurrentGameTime;
		float totalDayTime = GameController.instance.TotalDayTime;
		float colorLevel = currentGameTime / totalDayTime;
		if (currentGameTime > totalDayTime / 2)
		{
			colorLevel = 1 - colorLevel;
		}
		colorLevel = Mathf.Sin(2 * Mathf.PI * colorLevel);
		colorLevel = (colorLevel + 1) / 2;
		Camera.main.backgroundColor = new Color(colorLevel / 2, colorLevel / 2, colorLevel);
	}
}
