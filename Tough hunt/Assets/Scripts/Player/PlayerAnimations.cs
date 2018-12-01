using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    private SpriteRenderer sr;
<<<<<<< HEAD
    private Animator
=======
    private Animator ar;
>>>>>>> 1ee9ff9f388706641597cf5a8910844943d83397

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
