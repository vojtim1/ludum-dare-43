using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public static GameController instance;

	public int carryingFood;

	private float currentGameTime;
	public float CurrentGameTime
	{
		get
		{
			return currentGameTime;
		}

		/*set
		{
			currentGameTime = value;
		}*/
	}

	public float totalDayTime;

	// only for testing
	public Text testingText;

	// Use this for initialization
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	void Start () {
		CurrentGameTime = 0;
	}
	
	
	void Update () {
		CurrentGameTime += Time.deltaTime;
		if(CurrentGameTime >= totalDayTime)
		{
			CurrentGameTime -= totalDayTime;
		}
	}

	public void AddFood(int amount)
	{
		this.carryingFood += amount;
	}
}
