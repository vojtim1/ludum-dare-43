using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyCharacterController))]
public class DangerousMotor : MonoBehaviour {

    [SerializeField]
    private float speed = 40.0f;

    [SerializeField]
    private float escapeDistance = 60.0f;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float damage = 100;

    [SerializeField]
    private float attacksPerSecond = 1.0f;

    private float lastTimeAttacked = 0;

    // MOVEMENT
    private MyCharacterController controller;
    private float horizontalMove = 0f;
    private bool jump = false;


    // AI CONTROLLS
    bool isAttacking = false;
    bool isRunning = false;
    bool isWalking = false;

    Vector2 runningDirection = Vector3.zero;

    // IDLE
    float changeActionTime = 0;


    // Use this for initialization
    void Awake()
    {
        controller = GetComponent<MyCharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, escapeDistance);
    }

    void Update()
    {
        if (isAttacking && isRunning)
        {
            if (Vector2.Distance(player.transform.position, transform.position) >= escapeDistance)
            {
                runningDirection = Vector3.zero;
                isRunning = false;
            }
            if (Physics2D.Raycast(transform.position, runningDirection, runningDirection.magnitude * 3, 1 << 8).transform)
                jump = true;

            horizontalMove = runningDirection.x * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.x > gameObject.transform.position.x)
            {
                runningDirection = Vector2.right;
            }
            else
            {
                runningDirection = Vector2.left;
            }
            isRunning = true;
            isAttacking = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.x > gameObject.transform.position.x)
            {
                runningDirection = Vector2.right;
            }
            else
            {
                runningDirection = Vector2.left;
            }

            isAttacking = true;
            isRunning = true;
        }
    }

    void Attack()
    {
        if (lastTimeAttacked + (1 / attacksPerSecond) < Time.time)
        {
            Debug.Log("Attack!");
            lastTimeAttacked = Time.time;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
