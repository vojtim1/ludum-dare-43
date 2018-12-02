using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    public List<string> dialogues;

    [Header("Movement")]
    public bool isStatic;
    public bool standingStill; //For walking, false = walking, true = stop
    public Vector2[] movementPoints = new Vector2[2];
    public float time;
    public float stopTime;
    public Vector2 target; //Where does the NPC want to go -> inside bounds

    [Header("Animations")]
    public bool animate;
    private Animator animator;

    private bool initialized = false; //For gizmos, true only after Start() is called
    private SpriteRenderer spriteRenderer;

    void Start () {
        initialized = true;
        for (int i = 0; i < movementPoints.Length; i++)
        {
            movementPoints[i] += (Vector2)transform.position;
            movementPoints[i].y = transform.position.y;
        }
        SetNewTarget();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
	}
	
	void Update () {
        Move();
	}

    private void Move()
    {
        if (!isStatic)
        {
            if (standingStill)
            {
                if (animate)
                {
                    animator.CrossFade("Idle", 0);
                }

                time -= Time.deltaTime;

                if (time <= 0)
                {
                    standingStill = false;
                }
            }
            else
            {
                if (animate)
                {
                    animator.CrossFade("Walk", 0);
                }

                if(TargetDistance() <= 0.05f)
                {
                    standingStill = true;
                    time = stopTime;
                    SetNewTarget();
                }
                if(IsTargetRight())
                {
                    transform.position += (Vector3)(Vector2.right * Time.deltaTime);
                    spriteRenderer.flipX = false;
                }
                else
                {
                    transform.position += (Vector3)(Vector2.left * Time.deltaTime);
                    spriteRenderer.flipX = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TalkToHero();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!isStatic)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < movementPoints.Length; i++)
            {
                if (initialized)
                    Gizmos.DrawWireSphere(movementPoints[i], .1f);
                else Gizmos.DrawWireSphere((Vector3)movementPoints[i] + transform.position, .1f);
            }
        }
    }
    private bool IsTargetRight()
    {
        if (target.x > transform.position.x)
            return true;
        else return false;
    }
    private float TargetDistance()
    {
        return Vector2.Distance(transform.position, target);
    }
    private void SetNewTarget()
    {
        target = new Vector2(Random.Range(movementPoints[0].x, movementPoints[1].x), transform.position.y);
    }

    protected virtual void TalkToHero()
    {
        print(dialogues[Random.Range(0, dialogues.Count)]);
    }
    protected virtual void Interact() { }
}
