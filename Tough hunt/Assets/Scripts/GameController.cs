﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public int carryingFood;

	private float currentTime;
	public float totalDayTime;
	
	void Start () {
		
	}
	
	
	void Update () {
		currentTime += Time.deltaTime;
		if(currentTime >= totalDayTime)
		{
			currentTime -= totalDayTime;
		}
	}

	public void AddFood(int amount)
	{
		this.carryingFood += amount;
	}
}
