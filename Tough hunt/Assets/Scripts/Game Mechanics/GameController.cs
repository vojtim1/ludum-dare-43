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
	private bool nightEvaluated;
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

			if(Math.Round(currentGameTime) == Math.Round(totalDayTime / 2) && !nightEvaluated)
			{
				for (int i = 0; i < spawners.Count; i++)
				{
					List<GameObject> animals = spawners[i].GetAnimals();
					for (int j = 0; j < animals.Count; j++)
					{
						animals[j].SendMessage("NightBoost");
					}
				}
				Debug.Log("Boosted.");
				nightEvaluated = true;
			}
		} else if (skippingTime)
		{
			currentGameTime += Time.deltaTime * timeSpeedBoost;
		}
        // NEW DAY
		if (currentGameTime >= totalDayTime)
		{
			currentGameTime -= totalDayTime;
			currentDay++;
			raidEvaluated = false;
			nightEvaluated = false;
			village.NewDay();
            SpawnAnimals();
			for (int i = 0; i < spawners.Count; i++)
			{
				List<GameObject> animals = spawners[i].GetAnimals();
				for (int j = 0; j < animals.Count; j++)
				{
					animals[j].SendMessage("NewDayBoost", currentDay);
				}
			}
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

    private List<ISpawner> spawners;

    public void RegisterAnimalSpawner(ISpawner spawner)
    {
        if (spawners == null)
            spawners = new List<ISpawner>();

        spawners.Add(spawner);
    }

    void SpawnAnimals()
    {
        if (spawners == null)
            return;

        foreach (var spawner in spawners)
        {
            spawner.SpawnAnimals(currentDay);
        }
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
