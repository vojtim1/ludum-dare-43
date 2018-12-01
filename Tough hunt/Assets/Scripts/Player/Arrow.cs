using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour {

    // Use this for initialization
    float spawnTime;
    Rigidbody2D rb;


	void Start () {
        spawnTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > spawnTime + 10.0)
        {
            Destroy(this.gameObject);
        }
	}

    private void FixedUpdate()
    {
        var currentPosition = transform.position;
        var nextPosition = transform.position + new Vector3(rb.velocity.x, 0, rb.velocity.y);

        var deltaX = nextPosition.x - currentPosition.x;
        var deltaY = nextPosition.y - currentPosition.y;

        var rad = Mathf.Atan2(deltaY, deltaX);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Prey")
        {
            Destroy(collision.gameObject);
        }
    }
}
