using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    void Start()
    {

    }

    // Update is called once per frame
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

    void Shoot(float damage)
    {
        var currentCamera = Camera.main;

        Debug.Log(currentCamera);

        if (currentCamera != null)
        {
            GameObject arrowInstance;
            Vector3 projectileDirection = ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
            arrowInstance = Instantiate(arrow, transform.position + projectileDirection, Quaternion.Euler(Vector3.zero));
            arrowInstance.GetComponent<Rigidbody2D>().AddForce(projectileDirection * 2000);
            /*
            var mousePosition = currentCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = this.transform.position.z;

            var spawnedArrow = Instantiate(arrow, arrowSpawnPoint.position, Quaternion.Euler(mousePosition));
            spawnedArrow.GetComponent<Rigidbody2D>().AddForce((mousePosition - arrowSpawnPoint.position).normalized * arrowForce * damageMultiplierAdd);
            */
        }
        
    }
}
