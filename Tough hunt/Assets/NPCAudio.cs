using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NPCAudio : MonoBehaviour {
    AudioSource audioSource;
    public List<SoundsList> soundsLists;
    public float stepDistance;
    Vector2 lastPos;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        lastPos = transform.position;
	}
	
	void Update () {
        if (Vector2.Distance(lastPos, transform.position) >= stepDistance)
        {
            PlayStepSound();
            lastPos = transform.position;
        }
    }

    void PlayStepSound()
    {
        if (soundsLists[0].audioClips.Count > 0)
            audioSource.PlayOneShot(soundsLists[0].GetRandomClip(), soundsLists[0].GetVolume() * Settings.instance.GetVolume());
    }
}
