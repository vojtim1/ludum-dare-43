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

	public void NewDay(int currentDay)
	{
		currentFood -= dailyFoodIncome * currentDay;
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

	public void StartRaid()
	{
		if(Random.Range(0.0f, 1.0f) > raidChance)
		{
			Debug.Log("escaped");
			return;
		}
		raidChance = 0.1f;

		if (currentFood >= foodToSustainRaid)
		{
			// OK
		}
		else
		{
			GameController.instance.GameOver();
		}
	}
}
