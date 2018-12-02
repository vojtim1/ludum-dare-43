﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour {

    [SerializeField]
    private float health = 100.0f;
    [SerializeField]
    private int foodReward = 5;

    public bool alive = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<BoxCollider2D>().isTrigger = true;
        alive = false;
        GetComponent<Animator>().CrossFade("Death", 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !alive)
        {
            print("Yes");
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("Skinning " + gameObject.name + " for " + foodReward + " food!");
                Destroy(gameObject);
            }
        }
    }
}
