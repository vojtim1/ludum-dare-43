﻿using UnityEngine.UI;
using UnityEngine;

public class RabbitTrap : MonoBehaviour {
    public Sprite[] stateSprites = new Sprite[3]; //0 = empty, 1 = set up, 2 = trapped hare
    public float setUpTime = 2f;
    public float tickTime = 10f;
    public float triggerChance = 0.5f;
    public float minPlayerDistance;

    public float time;
    private bool settingUp = false;
    private TrapState state = TrapState.READY;

    SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (settingUp && state == TrapState.READY)
        {
            time += Time.deltaTime;
            if(time >= setUpTime)
            {
                settingUp = false;
                state = TrapState.SETUP;
                time = 0;
                UpdateSprite();
            }
        }
        if(state == TrapState.SETUP && PlayerIsFarEnough())
        {
            time += Time.deltaTime;
            if(time >= tickTime)
            {
                if (TickEvent())
                {
                    state = TrapState.TRIGGERED;
                    UpdateSprite();
                }
                else time = 0;
            }
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (state == TrapState.READY)
            {
                if(Settings.instance.GetShowHints())
                    TextBubble.instance.Say("Hmmm a hare hole...\nI might set up a trap here!", collision.gameObject, 2);
                time = 0;
            }
            if(state == TrapState.TRIGGERED)
            {
                if (Settings.instance.GetShowHints())
                    TextBubble.instance.Say("A hare got caught, nice...\nLet's gather the food!", collision.gameObject, 2);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (state == TrapState.READY)
                {
                    settingUp = true;
                    time = 0;
                }
                if (state == TrapState.TRIGGERED)
                {
                    print("Gathered!");
                    state = TrapState.READY;
                    time = 0;
                    UpdateSprite();
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (state == TrapState.READY && settingUp)
            {
                settingUp = false;
                time = 0;
            }
        }
    }
    private bool PlayerIsFarEnough()
    {
        if (Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= minPlayerDistance)
            return true;
        return false;
    }
    private bool TickEvent()
    {
        float random = Random.Range(0f,1f);
        if (random > triggerChance)
            return false;
        else return true;
    }
    private void UpdateSprite()
    {
        sr.sprite = stateSprites[(int)state];
    }
}
