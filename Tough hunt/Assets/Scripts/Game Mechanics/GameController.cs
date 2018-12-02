﻿using System;
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

		set
		{
			currentGameTime = value;
		}
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
	private float timeSpeed;
	public float TimeSpeed
	{
		get
		{
			return timeSpeed;
		}

		set
		{
			timeSpeed = value;
		}
	}
	private bool gamePaused = false;
	public bool GamePaused
	{
		get
		{
			return gamePaused;
		}

		set
		{
			gamePaused = value;
		}
	}
	private int currentDay;

	private float distanceToAllowRaid;

	private Village village;
	private GameObject player;
	private Player playerScript;

	public MessageBoard messageboard;

	public GameObject foodDisplay;

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
		timeSpeed = 1;
		currentDay = 0;

		village = Village.instance;
		player = GameObject.FindGameObjectWithTag("Player");
		playerScript = Player.instance;

		distanceToAllowRaid = Screen.width * 2;

		raidEvaluated = false;

		messageboard.gameObject.SetActive(true);
		messageboard.DisplayText("Welcome to the game.","Welcome text.");
	}

	private bool raidEvaluated;
	void Update () {
		if (!gamePaused)
		{
			currentGameTime += Time.deltaTime * timeSpeed;
			if (currentGameTime >= totalDayTime)
			{
				currentGameTime -= totalDayTime;
				currentDay++;
				raidEvaluated = false;
				village.NewDay();
			}
			if (Math.Round(currentGameTime) == Math.Round(totalDayTime / 4) && !raidEvaluated)
			{
				//if (Math.Abs(player.transform.position.x - this.transform.position.x) > distanceToAllowRaid)
				{
					village.StartRaid(currentDay);
					raidEvaluated = true;
				}
			}
		}
	}

	public void KeepTheLoot()
	{
		playerScript.Boost(carryingFood);
		carryingFood = 0;
		foodDisplay.SendMessage("SetText", carryingFood);
	}

	public void SacrificeTheLoot()
	{
		village.AddFood(carryingFood);
		carryingFood = 0;
		foodDisplay.SendMessage("SetText", carryingFood);
	}

	public void AddFood(int amount)
	{
		this.carryingFood += amount;
		foodDisplay.SendMessage("SetText", carryingFood);
	}

	public void RegainResources()
	{
		playerScript.RegainResources();
	}

	public void PauseGame()
	{
		gamePaused = true;
	}

	public void UnPauseGame()
	{
		gamePaused = false;
	}

	public void GameOver(string deathType)
	{
		PauseGame();
		messageboard.gameObject.SetActive(true);
		// TODO: Replace MessageBoard with GameOver screen
		// TODO: Use Enum instead of string deathType
		switch (deathType)
		{
			case "playerDead":
				messageboard.DisplayText("Game over!", "You died. The game is over.");
				break;
			case "villageRaided":
				messageboard.DisplayText("Game over!", "The village was raided. The game is over.");
				break;
		}
	}
}
