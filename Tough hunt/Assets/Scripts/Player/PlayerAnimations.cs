using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    private SpriteRenderer sr;
    private Animator ar;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        ar = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            ar.CrossFade("Walk", 0);
            if (!GetComponent<PlayerMotor>().IsGoingRight())
                sr.flipX = true;
            else sr.flipX = false;
        }
        else ar.CrossFade("Idle", 0);
    }
}
