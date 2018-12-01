using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyCharacterController))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private float walkSpeed = 10.0f;

    [SerializeField]
    private float runSpeed = 40.0f;

  
    private MyCharacterController controller;
    private float horizontalMove = 0f;
    private bool jump = false;
    

    // Use this for initialization
    void Awake()
    {
        controller = GetComponent<MyCharacterController>();
    }

    void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

	}


    public bool IsGoingRight()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
            return true;
        else
            return false;
    }

    // Update is called once per frame
    void FixedUpdate () {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
	}
}
