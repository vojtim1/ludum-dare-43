using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MyCharacterController : MonoBehaviour
{

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

    [SerializeField]
    private GameObject spriteObject;
    private SpriteRenderer spriteRenderer;

    private bool grounded = false;
    private Rigidbody2D rigidbody2D;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
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

        if (rigidbody2D.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rigidbody2D.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (grounded && jump)
        {
            //rigidbody2D.velocity = Vector3.zero;
            //rigidbody2D.angularVelocity = 0;
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }

        //Debug.Log(IsGoingRight());
    }


}
