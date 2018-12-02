using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyCharacterController))]
public class PlayerMotor : MonoBehaviour {

    private float runSpeed;
	public float RunSpeed
	{
		get
		{
			return runSpeed;
		}

		set
		{
			runSpeed = value;
		}
	}

	private MyCharacterController controller;
    private float horizontalMove = 0f;
    private bool jump = false;

	private bool gamePaused = false;
	public bool GamePaused
	{
		get
		{
			return gamePaused;
		}

		set
		{
			gamePaused = value;
		}
	}

	void Awake()
    {
        controller = GetComponent<MyCharacterController>();
		Player.instance.playerMotor = this;
    }

    void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump") && !gamePaused)
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

    void FixedUpdate () {
		if (!gamePaused)
		{
			controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		}
        jump = false;
	}
}
