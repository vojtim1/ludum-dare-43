using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour {

    float spawnTime;
    Rigidbody2D rb;
    Vector2 direction;
    bool rotate = true;

	[SerializeField]
	AudioSource audioSource;
	[SerializeField]
	private AudioClip hitGround;
	[SerializeField]
	private AudioClip hitMeat;

	void Start () {
        spawnTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
        if (Time.time > spawnTime + 10.0)
        {
            Destroy(gameObject);
        }
	}

    private void FixedUpdate()
    {
        if (rotate && transform.parent == null)
        {
            var nextPosition = transform.position + (Vector3)rb.velocity * 100;
            var angle = Mathf.Atan2(nextPosition.y, nextPosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if(transform.parent)
        {
            if (!transform.parent.GetComponent<Prey>().alive) //Destroy on animal death
                Destroy(gameObject);
        }
    }

	private float damage;
	public void SetDamage(float damage)
	{
		this.damage = damage;
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)//If is prey
        {
            transform.SetParent(collision.transform);
            collision.gameObject.SendMessage("TakeDamage", damage);
			audioSource.PlayOneShot(hitMeat);
        }
		if(collision.gameObject.layer == 8)
		{
			audioSource.PlayOneShot(hitGround);
		}
        if(collision.gameObject.GetComponent<SpriteRenderer>())
            GetComponent<SpriteRenderer>().sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
        transform.position += (Vector3)GetComponent<Rigidbody2D>().velocity * Time.deltaTime * 2;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;
        gameObject.tag = "Arrow";
        rotate = false;
    }
}
