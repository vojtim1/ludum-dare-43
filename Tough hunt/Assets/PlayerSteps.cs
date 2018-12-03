using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSteps : MonoBehaviour {
    public List<SoundsList> soundsLists;
    public float stepDistance = 0.4f;

    private AudioSource audioSource;
    private Vector2 lastPos;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        lastPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!GetComponent<MyCharacterController>().grounded)
            return;
		if(Vector2.Distance(lastPos, transform.position) >= stepDistance)
        {
            PlayStepSound();
            lastPos = transform.position;
        }
	}

    void PlayStepSound()
    {
        audioSource.PlayOneShot(soundsLists[0].GetRandomClip(), soundsLists[0].GetVolume());
    }
    void TakeDamage()
    {
        audioSource.PlayOneShot(soundsLists[1].GetRandomClip(), soundsLists[1].GetVolume());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            //PlayStepSound();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            //PlayStepSound();
        }
    }
}
