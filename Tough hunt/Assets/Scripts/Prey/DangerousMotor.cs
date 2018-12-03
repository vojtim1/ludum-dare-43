using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyCharacterController))]
public class DangerousMotor : MonoBehaviour {

    public bool agro = false;
    public float agroTime = 10;
    private float agroNow = 0;

    [SerializeField]
    private float speed = 100.0f;

    [SerializeField]
    private float agroDistance = 10.0f;

    [SerializeField]
    private float attackDistance = 3f;

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
        Gizmos.DrawWireSphere(gameObject.transform.position, agroDistance);
    }

    void Update()
    {
        if(GetComponent<Prey>().alive)
        {
            horizontalMove = 0;
            if (DistanceFromPlayer() <= agroDistance)
            {
                agro = true;
                agroNow = agroTime;
            }
            if(agro)
            {
                agroNow -= Time.deltaTime;
                if (agroNow <= 0)
                {
                    agro = false;
                }
                if(DistanceFromPlayer() <= attackDistance)
                {
                    Attack();
                }
                else
                {
                    runningDirection = GetRunningDirection(player.gameObject);
                    horizontalMove = runningDirection.x * speed;
                    if (obstacleAhead())
                        jump = true;
                }
            }
        }
    }

	[SerializeField]
	float nightBoost;
	public void NightBoost()
	{
		speed = speed * nightBoost;
		agroDistance = agroDistance * nightBoost;
		damage = damage * nightBoost;
		attacksPerSecond = attacksPerSecond * nightBoost;
	}

	[SerializeField]
	float newDayBoost;
	public void DayNerf(int dayNumber)
	{
		speed = speed * newDayBoost * dayNumber;
		damage = damage * newDayBoost * dayNumber;
		attacksPerSecond = attacksPerSecond * newDayBoost * dayNumber;
	}

	Vector2 GetRunningDirection(GameObject lGameObject)
    {
        if (lGameObject.transform.position.x > gameObject.transform.position.x)
            return Vector2.right;
        else return Vector2.left;
    }

    void Attack()
    {
        if (lastTimeAttacked + (1 / attacksPerSecond) < Time.time)
        {
            lastTimeAttacked = Time.time;
            GetComponent<Animator>().CrossFade("Attack", 0);
            Player.instance.SendMessage("TakeDamage", damage);
            Player.instance.SendMessage("Push", gameObject);
            GetComponent<PreyAudio>().PlayAttackSound();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    float DistanceFromPlayer()
    {
        return Vector2.Distance(player.transform.position, transform.position);
    }

    void TakeDamage()
    {
        agro = true;
        agroNow = agroTime;
    }
    bool obstacleAhead()
    {
        bool r = (Physics2D.Raycast(transform.position, runningDirection, runningDirection.magnitude * 1.5f, 1 << 8).transform);
        return r;
    }
}
