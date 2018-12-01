using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{

	public static Village instance;

	private int currentFood;
	public int dailyFoodIncome;
	public int foodToSustainRaid;

	public float raidChance;

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

	private void Start()
	{
		currentFood = 0;
	}

	public void NewDay()
	{
		currentFood -= dailyFoodIncome;
		if(currentFood < 0)
		{
			currentFood = 0;
		}

		raidChance += 0.1f;
	}

	public void AddFood(int amount)
	{
		currentFood += amount;
	}

	public void StartRaid(int currentDay)
	{
		if(Random.Range(0.0f, 1.0f) > raidChance)
		{
			Debug.Log("escaped");
			return;
		}
		raidChance = 0.1f;

		if (currentFood >= foodToSustainRaid * currentDay)
		{
			// OK
		}
		else
		{
			GameController.instance.GameOver();
		}
	}
}
