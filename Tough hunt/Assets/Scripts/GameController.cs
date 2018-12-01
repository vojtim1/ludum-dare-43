using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public int carryingFood;

	private float currentTime;
	public float totalTime;
	
	void Start () {
		
	}
	
	
	void Update () {
		
	}

	public void AddFood(int amount)
	{
		this.carryingFood += amount;
	}
}
