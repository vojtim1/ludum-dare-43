using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {
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
        audioSource.PlayOneShot(soundsLists[0].GetRandomClip(), soundsLists[0].GetVolume() * UpdateVolume());
    }
    public void PlayHurtSound()
    {
        audioSource.PlayOneShot(soundsLists[1].GetRandomClip(), soundsLists[1].GetVolume() * UpdateVolume());
    }
    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(soundsLists[2].GetRandomClip(), soundsLists[2].GetVolume() * UpdateVolume());
    }
    float UpdateVolume()
    {
        return Settings.instance.GetVolume();
    }
}
