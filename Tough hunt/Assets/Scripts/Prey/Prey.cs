using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour {

    [SerializeField]
    private float health = 100.0f;
    [SerializeField]
    private int foodReward = 5;

    public Collider2D harvestTrigger;//Collider activated after death

    public bool alive = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<MyCharacterController>().grounded)
            if (!alive)
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

	[SerializeField]
	float nightBoost;
	public void NightBoost()
	{
		health = health * nightBoost;
	}

	[SerializeField]
	float newDayBoost;
	public void NewDayBoost(int dayNumber)
	{
		health = health * newDayBoost * dayNumber;
	}

	void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else GetComponent<PreyAudio>().PlayHurtSound();
    }

    void Die()
    {
        if (GetComponent<MyCharacterController>().grounded)
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        foreach (Collider2D col in GetComponents<Collider2D>())
            col.enabled = false;
        harvestTrigger.enabled = true;
        harvestTrigger.isTrigger = true;
        alive = false;
        GetComponent<Animator>().CrossFade("Death", 0);
        GetComponent<PreyAudio>().PlayDeathSound();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !alive)
        {
            TextBubble.instance.Say("I might skin this animal...", collision.gameObject, 2);
            if (Input.GetKey(KeyCode.E))
            {
                GameController.instance.AddFood(foodReward);
                Destroy(gameObject);
            }
        }
    }
}
