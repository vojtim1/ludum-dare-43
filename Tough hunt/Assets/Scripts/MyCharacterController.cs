using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MyCharacterController : MonoBehaviour
{
    public bool rotate = true;
    public bool animate = false;
    [Range(0, .3f)]
    [SerializeField]
    private float movementSmoothing = .05f;

    [SerializeField]
    private float jumpForce = 400f;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundedRadius;

    private bool grounded = false;
    private Rigidbody2D rigidbody2D;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundedRadius);
    }

    private void FixedUpdate()
    {
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
    }

    public void Move(float move, bool jump)
    {
        Vector3 targetVelocity = new Vector2(move, rigidbody2D.velocity.y);
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (rotate)
        {
            if (rigidbody2D.velocity.x != 0)
            {
                if (rigidbody2D.velocity.x > 0.1f)
                    GetComponent<SpriteRenderer>().flipX = false;
                if (rigidbody2D.velocity.x < -0.1f)
                    GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if(animate)
        {
            Animator animator = GetComponent<Animator>();
            if (rigidbody2D.velocity.magnitude > 0.1f)
            {
                //if (rigidbody2D.velocity.x > 0.1f)
                //{
                animator.CrossFade("Walk", 0);
                //}
                //else animator.CrossFade("Walk_backwards", 0);
            }
            else animator.CrossFade("Idle", 0);
        }
           

        if (grounded && jump)
        {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }
}
