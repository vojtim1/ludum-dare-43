using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    public List<string> dialogues;

    [Header("Movement")]
    public bool isStatic;
    public bool standingStill; //For walking, false = walking, true = stop
    public float speed;
    public Vector2[] movementPoints = new Vector2[2];
    public float time;
    public float stopTime;
    public Vector2 target; //Where does the NPC want to go -> inside bounds

    private bool initialized = false; //For gizmos, true only after Start() is called

    void Start () {
        initialized = true;
        for (int i = 0; i < movementPoints.Length; i++)
        {
            movementPoints[i] += (Vector2)transform.position;
        }
	}
	
	void Update () {
		if(!isStatic)
        {
            if(standingStill)
            {
                time -= Time.deltaTime;
                if(time <= 0)
                {
                    standingStill = false;
                }
            }
            else
            {

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

    protected virtual void TalkToHero() { }
    protected virtual void Interact() { }
}
