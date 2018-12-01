using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player instance;

	[SerializeField]
    GameObject arrow;

    [SerializeField]
    Transform arrowSpawnPoint;

    public float damage = 100.0f;

    public float health = 100.0f;

    float currentDamageMultiplier = 0.0f;
    float damageMultiplierAdd = 0.7f;

    float holdingMaxTime = 2;
    float holdingTime = 0;
    [SerializeField]
    Image holdIndicator;

    [SerializeField]
    float arrowForce = 10;

    bool isHolding = false;

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

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            holdingTime = 0;
            isHolding = true;
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            isHolding = false;
            Shoot(100, holdingTime/holdingMaxTime);
        }
        if(isHolding)
        {
            holdingTime += Time.deltaTime;
            if (holdingTime >= holdingMaxTime)
                holdingTime = holdingMaxTime;
            holdIndicator.fillAmount = holdingTime / holdingMaxTime;
        }
    }

	public void Boost(int amount)
	{
		
	}

	public void RegainResources()
	{

	}

    void Shoot(float damage, float timeMultiplier)
    {
        var currentCamera = Camera.main;

        Debug.Log(currentCamera);

        if (currentCamera != null)
        {
            GameObject arrowInstance;
            Vector3 projectileDirection = ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
            projectileDirection.z = 0;

            float maxMagnitude = 0.5f;

            float multiplier = (maxMagnitude * timeMultiplier) / projectileDirection.magnitude;

            if (projectileDirection.magnitude != maxMagnitude * timeMultiplier)
                projectileDirection *= multiplier;

            print(projectileDirection.magnitude);

            arrowInstance = Instantiate(arrow, transform.position + projectileDirection, Quaternion.Euler(Vector3.zero));
            arrowInstance.GetComponent<Rigidbody2D>().AddForce(projectileDirection * 2000);
        }
    }
}
