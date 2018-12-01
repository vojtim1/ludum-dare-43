using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    private SpriteRenderer sr;
    private Animator

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            GetComponent<Animator>().CrossFade("Walk", 0);
            if (!GetComponent<PlayerMotor>().IsGoingRight())
                GetComponent<SpriteRenderer>().flipX = true;
            else GetComponent<SpriteRenderer>().flipX = false;
        }
        else GetComponent<Animator>().CrossFade("Idle", 0);
    }
}
