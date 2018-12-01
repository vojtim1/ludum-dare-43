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
	[SerializeField]
	private float totalDayTime;
	public float TotalDayTime
	{
		get
		{
			return totalDayTime;
		}

		/*set
		{
			totalDayTime = value;
		}*/
	}
	private int currentDay;

	private Village village;

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
		currentGameTime = 0;
		currentDay = 0;

		village = Village.instance;

		raidEvaluated = false;
	}

	private bool raidEvaluated;
	void Update () {
		currentGameTime += Time.deltaTime;
		if(currentGameTime >= totalDayTime)
		{
			currentGameTime -= totalDayTime;
			currentDay++;
			raidEvaluated = false;
			village.NewDay();
		}
		if(Math.Round(currentGameTime) == Math.Round(totalDayTime / 4) && !raidEvaluated)
		{
			// TODO: check that player is far enough
			village.StartRaid(currentDay);
			raidEvaluated = true;
		}
	}

	public void AddFood(int amount)
	{
		this.carryingFood += amount;
	}

	public void GameOver()
	{
		Debug.Log("The village was raided!");
	}
}
