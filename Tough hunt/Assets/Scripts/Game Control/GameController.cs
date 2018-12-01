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
		currentGameTime = 0;
	}
	
	
	void Update () {
		currentGameTime += Time.deltaTime;
		if(currentGameTime >= totalDayTime)
		{
			currentGameTime -= totalDayTime;
		}
	}

	public void AddFood(int amount)
	{
		this.carryingFood += amount;
	}
}
