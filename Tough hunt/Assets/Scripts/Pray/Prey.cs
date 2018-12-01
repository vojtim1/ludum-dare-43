using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour {

    [SerializeField]
    private float health = 100.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
