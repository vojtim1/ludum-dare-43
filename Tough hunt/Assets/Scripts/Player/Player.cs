﻿using System.Collections;
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
            GameObject _arrowPrefab;
            Vector3 projectileDirection = ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
            _arrowPrefab = Instantiate(arrow, transform.position + projectileDirection * 250, Quaternion.Euler(Vector3.zero));
            _arrowPrefab.transform.position = new Vector3(_arrowPrefab.transform.position.x, _arrowPrefab.transform.position.y, 0);
            _arrowPrefab.GetComponent<Rigidbody2D>().AddForce(projectileDirection * 50);
            /*
            var mousePosition = currentCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = this.transform.position.z;

            var spawnedArrow = Instantiate(arrow, arrowSpawnPoint.position, Quaternion.Euler(mousePosition));
            spawnedArrow.GetComponent<Rigidbody2D>().AddForce((mousePosition - arrowSpawnPoint.position).normalized * arrowForce * damageMultiplierAdd);
            */
        }
        
    }
}
