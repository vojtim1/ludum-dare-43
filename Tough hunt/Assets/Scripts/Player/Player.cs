using System.Collections;
using System.Collections.Generic;
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
        if (Input.GetMouseButtonDown(0))
        {
            isHolding = true;
            if (damageMultiplierAdd < 1)
            {
                damageMultiplierAdd += damageMultiplierAdd * Time.deltaTime;
            }
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isHolding)
            {
                Shoot(damage * currentDamageMultiplier);
                currentDamageMultiplier = 0;
                isHolding = false;
            }
        }
    }

	public void Boost(int amount)
	{
		
	}

	public void RegainResources()
	{

	}

    void Shoot(float damage)
    {
        var currentCamera = Camera.main;

        Debug.Log(currentCamera);

        if (currentCamera != null)
        {
            var mousePosition = currentCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = this.transform.position.z;

            var spawnedArrow = Instantiate(arrow, arrowSpawnPoint.position, Quaternion.Euler(mousePosition));
            spawnedArrow.GetComponent<Rigidbody2D>().AddForce((mousePosition - arrowSpawnPoint.position).normalized * arrowForce * damageMultiplierAdd);
        }
        
    }
}
