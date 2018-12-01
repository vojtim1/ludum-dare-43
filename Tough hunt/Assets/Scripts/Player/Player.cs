using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player instance;

	public PlayerMotor playerMotor;

	[SerializeField]
	float maxHealth;
	[SerializeField]
	float speed;
	[SerializeField]
	float arrowDamage;
	[SerializeField]
	float arrowSpeed;
	[SerializeField]
	float maxArrowCount;

	private float currentHealth;
	private int currentArrowCount;

	float holdingMaxTime = 2;
	float holdingTime = 0;
	[SerializeField]
	Image holdIndicator;

	[SerializeField]
	GameObject arrow;

	[SerializeField]
	Transform arrowSpawnPoint;

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
		playerMotor.RunSpeed = speed;
		RegainResources();
	}

	bool isHolding = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			holdingTime = 0;
			isHolding = true;
			playerMotor.RunSpeed = speed / 4;
		}
		if(Input.GetKeyUp(KeyCode.Mouse0))
		{
			isHolding = false;
			Shoot(arrowDamage, holdingTime/holdingMaxTime);
			playerMotor.RunSpeed = speed;
		}
		if(isHolding)
		{
			holdingTime += Time.deltaTime;
			if (holdingTime >= holdingMaxTime)
				holdingTime = holdingMaxTime;
			holdIndicator.fillAmount = holdingTime / holdingMaxTime;
		}
	}

	[SerializeField]
	private int boostModifier;
	public void Boost(int foodAmount)
	{
		float baseToAdd = foodAmount * boostModifier;
		maxHealth += baseToAdd;
		speed += baseToAdd;
		arrowDamage += baseToAdd;
		arrowSpeed += baseToAdd * 10f;
		maxArrowCount += baseToAdd * 0.1f;
		holdingMaxTime -= baseToAdd * 0.005f;
	}

	public void RegainResources()
	{
		currentHealth = maxHealth;
		currentArrowCount = (int)Mathf.Round(maxArrowCount);
	}

	void Shoot(float damage, float timeMultiplier)
	{
		if(currentArrowCount > 0)
		{
			currentArrowCount--;
			GameObject arrowInstance;
			Vector3 projectileDirection = ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
			projectileDirection.z = 0;

			float maxMagnitude = 0.5f;

			float multiplier = (maxMagnitude * timeMultiplier) / projectileDirection.magnitude;

			if (projectileDirection.magnitude != maxMagnitude * timeMultiplier)
				projectileDirection *= multiplier;

			arrowInstance = Instantiate(arrow, transform.position + projectileDirection, Quaternion.Euler(Vector3.zero));
			arrowInstance.GetComponent<Rigidbody2D>().AddForce(projectileDirection * arrowSpeed);
		}
	}
}
