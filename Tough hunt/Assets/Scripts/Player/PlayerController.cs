using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    [Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;

    private Rigidbody2D rigidbody;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move(float move, bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * 10, rigidbody.velocity.y);
        rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, MovementSmoothing);
    }
}
