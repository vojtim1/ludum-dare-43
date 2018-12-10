using System.Collections.Generic;
using UnityEngine;

public class PreyAudio : MonoBehaviour {
    public List<SoundsList> soundsLists;
    public float stepDistance;

    private AudioSource audioSource;
    private Vector2 lastPos;
    private float afterDeathSoundsTime = 0.4f;
    private float time;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        lastPos = transform.position;
	}
	
	void Update () {
        if (!audioSource)
            return;
        if (!GetComponent<MyCharacterController>().grounded)
            return;
        if (Vector2.Distance(lastPos, transform.position) >= stepDistance)
        {
            PlayStepSound();
            lastPos = transform.position;
        }
        if(!GetComponent<Prey>().alive)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                PlayAfterDeathSound();
                time = afterDeathSoundsTime;
            }
        }
    }

    void PlayStepSound()
    {
        if (soundsLists[0].audioClips.Count > 0)
            audioSource.PlayOneShot(soundsLists[0].GetRandomClip(), soundsLists[0].GetVolume() * UpdateVolume());
    }
    public void PlayHurtSound()
    {
        if (soundsLists[1].audioClips.Count > 0)
            audioSource.PlayOneShot(soundsLists[1].GetRandomClip(), soundsLists[1].GetVolume() * UpdateVolume());
    }
    public void PlayDeathSound()
    {
        if(soundsLists[2].audioClips.Count > 0)
            audioSource.PlayOneShot(soundsLists[2].GetRandomClip(), soundsLists[2].GetVolume() * UpdateVolume());
    }
    void PlayAfterDeathSound()
    {
        if (soundsLists[3].audioClips.Count > 0)
            audioSource.PlayOneShot(soundsLists[3].GetRandomClip(), soundsLists[3].GetVolume() * UpdateVolume());
    }
    public void PlayAttackSound()
    {
        if (soundsLists[4].audioClips.Count > 0)
            audioSource.PlayOneShot(soundsLists[4].GetRandomClip(), soundsLists[4].GetVolume() * UpdateVolume());
    }
    float UpdateVolume()
    {
        return Settings.instance.GetVolume();
    }
}
