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
	[SerializeField]
	private float timeSpeedBoost;
	private bool skippingTime;
	public bool SkippingTime
	{
		get
		{
			return skippingTime;
		}

		set
		{
			skippingTime = value;
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
		currentDay = 0;

		village = Village.instance;
		player = GameObject.FindGameObjectWithTag("Player");
		playerScript = Player.instance;

		distanceToAllowRaid = Screen.width * 2;

		raidEvaluated = false;

		foodDisplay.SendMessage("SetText", carryingFood);
		messageboard.DisplayText("Welcome to the game.","Welcome text.", true);
	}

	private bool raidEvaluated;
	void Update () {
		if (!gamePaused)
		{
			currentGameTime += Time.deltaTime;
			if (Math.Round(currentGameTime) == Math.Round(totalDayTime / 4) && !raidEvaluated)
			{
				// TODO: add evaluation only if player is far enough
				village.StartRaid(currentDay);
				raidEvaluated = true;
			}
		} else if (skippingTime)
		{
			currentGameTime += Time.deltaTime * timeSpeedBoost;
		}
		if (currentGameTime >= totalDayTime)
		{
			currentGameTime -= totalDayTime;
			currentDay++;
			raidEvaluated = false;
			village.NewDay();
			skippingTime = false;
			UnPauseGame();
		}
	}

	public void SkipTime()
	{
		PauseGame();
		skippingTime = true;
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

	public void GameOver(GameOverState gameOverState)
	{
		PauseGame();
		string gameOverMessage = "";
		// TODO: Replace MessageBoard with GameOver screen
		switch (gameOverState)
		{
			case GameOverState.PLAYERDIED:
				gameOverMessage = "You died. The game is over.";
				break;
			case GameOverState.VILLAGERAIDED:
				gameOverMessage = "The village was raided. The game is over.";
				break;
		}
		messageboard.DisplayText("Game over!", gameOverMessage, false);
	}
}
