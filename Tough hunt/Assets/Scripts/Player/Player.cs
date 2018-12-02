using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player instance;

	public PlayerMotor playerMotor;
    public GameObject rootBone;

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

	[SerializeField]
	float holdingMaxTime;
	float holdingTime = 0;
	[SerializeField]
	Image holdIndicator;

	[SerializeField]
	Text healthText;
	[SerializeField]
	Image healthImage;

	[SerializeField]
	GameObject arrow;

	[SerializeField]
	AudioSource arrowShot;

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
		if (!GameController.instance.GamePaused)
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				holdingTime = 0;
				isHolding = true;
				playerMotor.RunSpeed = speed / 4f;
			}
			if (Input.GetKeyUp(KeyCode.Mouse0))
			{
				if (isHolding)
				{
					Shoot(arrowDamage, holdingTime / holdingMaxTime);
					playerMotor.RunSpeed = speed;
                    holdingTime = 0;
                    isHolding = false;
                }
			}
			if (isHolding)
			{
				holdingTime += Time.deltaTime;
				if (holdingTime >= holdingMaxTime)
					holdingTime = holdingMaxTime;
			}
            holdIndicator.fillAmount = holdingTime / holdingMaxTime;
            PullArmMovementIK.instance.SetPullMultiplier(holdingTime / holdingMaxTime);

			if ((Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x))
				gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
			else gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
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

		currentHealth = maxHealth;
		healthText.SendMessage("SetText", currentHealth);
	}

	public void RegainResources()
	{
		currentHealth = maxHealth;
		currentArrowCount = (int)Mathf.Round(maxArrowCount);

		healthText.SendMessage("SetText", currentHealth);
		healthImage.color = new Color(1,1,1, 1 - currentHealth / maxHealth);
	}

	void Shoot(float damage, float timeMultiplier)
	{
		if(currentArrowCount > 0)
		{
			currentArrowCount--;

			arrowShot.Play();

			GameObject arrowInstance;
			Vector3 projectileDirection = ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
			projectileDirection.z = 0;

			float maxMagnitude = 0.5f;

			float multiplier = (maxMagnitude * timeMultiplier) / projectileDirection.magnitude;

			if (projectileDirection.magnitude != maxMagnitude * timeMultiplier)
				projectileDirection *= multiplier;

			arrowInstance = Instantiate(arrow, transform.position + projectileDirection, Quaternion.Euler(Vector3.zero));
			arrowInstance.SendMessage("SetDamage", arrowDamage);
			arrowInstance.GetComponent<Rigidbody2D>().AddForce(projectileDirection * arrowSpeed);
		}
	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;
		healthText.SendMessage("SetText", currentHealth);
		healthImage.color = new Color(1, 1, 1, 1 - currentHealth / maxHealth);
		if (currentHealth <= 0)
		{
			GameController.instance.GameOver("playerDead");
		}
	}
}
