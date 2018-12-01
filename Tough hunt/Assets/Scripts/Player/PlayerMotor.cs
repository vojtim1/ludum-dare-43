using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMotor : MonoBehaviour {

    public PlayerController controller;

    float horizontalMove = 0f;

    // Use this for initialization
    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        controller.Move(horizontalMove, false);
	}
}
