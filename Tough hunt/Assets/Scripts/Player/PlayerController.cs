using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    [Range(0, .3f)]
    [SerializeField]
    private float MovementSmoothing = .05f;

    [SerializeField]
    private float Speed = 10.0f;

    private Rigidbody2D rigidbody;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
	
    public void Move(float move, bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * Speed, rigidbody.velocity.y);
        rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, MovementSmoothing);
    }
}
