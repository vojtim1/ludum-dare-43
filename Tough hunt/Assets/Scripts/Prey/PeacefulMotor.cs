using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyCharacterController))]
public class PeacefulMotor : MonoBehaviour {

    [SerializeField]
    private float speed = 40.0f;

    [SerializeField]
    private float escapeDistance = 60.0f;

    [SerializeField]
    private Transform player;

    private MyCharacterController controller;
    private float horizontalMove = 0f;
    private bool jump = false;


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
        if (isRunning)
        {
            if (Vector2.Distance(player.position, transform.position) >= escapeDistance)
            {
                isRunning = false;
                runningDirection = Vector3.zero;
            }
            if (Physics2D.Raycast(transform.position, Vector2.left, Vector2.left.magnitude * 2, 1 << 8).transform)
            {
                print(Physics2D.Raycast(transform.position, Vector2.left, Vector2.left.magnitude * 2, 1 << 8).transform);
                jump = true;
            }
            horizontalMove = runningDirection.x * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.x > gameObject.transform.position.x)
            {
                runningDirection = Vector2.left;
            }
            else
            {
                runningDirection = Vector2.right;
            }

            isRunning = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
