using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyCharacterController))]
public class PeacefulMotor : MonoBehaviour {

    [SerializeField]
    private float speed = 40.0f;

    [SerializeField]
    private float fearDistance = 60.0f;

    [SerializeField]
    private Transform player;

    private MyCharacterController controller;
    private float horizontalMove = 0f;
    private bool jump = false;


    bool isRunning = false;
    bool isWalking = false;
    Vector2 runningDirection = Vector3.zero;

    [Header("Fear settings")]
    public bool fear = false;
    public float fearTime;
    private float fearNow;


    // Use this for initialization
    void Awake()
    {
        controller = GetComponent<MyCharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, fearDistance);
    }

    void Update()
    {
        if (GetComponent<Prey>().alive)
        {
            horizontalMove = 0;
            if (DistanceFromPlayer() <= fearDistance)
            {
                fear = true;
                fearNow = fearTime;
            }
            foreach(GameObject arrow in GameObject.FindGameObjectsWithTag("Arrow"))
            {
                if(IsObjectInFearDistance(arrow))
                {
                    fear = true;
                    fearNow = fearTime;
                }
            }
            if (fear)
            {
                fearNow -= Time.deltaTime;
                if (fearNow <= 0)
                {
                    fear = false;
                }
                runningDirection = GetRunningDirection(player.gameObject);
                horizontalMove = runningDirection.x * speed;
                if (obstacleAhead())
                    jump = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fear = true;
            fearNow = fearTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    Vector2 GetRunningDirection(GameObject lGameObject)
    {
        if (lGameObject.transform.position.x < gameObject.transform.position.x)
            return Vector2.right;
        else return Vector2.left;
    }

    float DistanceFromPlayer()
    {
        return Vector2.Distance(player.transform.position, transform.position);
    }

    bool IsObjectInFearDistance(GameObject go)
    {
        if (Vector2.Distance(go.transform.position, transform.position) <= fearDistance)
            return true;
        else return false;
    }

    void TakeDamage()
    {
        fear = true;
        fearNow = fearTime;
    }
    bool obstacleAhead()
    {
        bool r = (Physics2D.Raycast(transform.position, runningDirection, runningDirection.magnitude * 1.5f, 1 << 8).transform);
        return r;
    }
}
