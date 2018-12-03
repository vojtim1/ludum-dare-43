using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundsDayNight : MonoBehaviour {
    public AudioClip dayClip;
    public AudioClip nightClip;

    private bool day = true;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (GameController.instance.CurrentGameTime > GameController.instance.TotalDayTime / 2 && day)
            ChangeClip();
        else if (GameController.instance.CurrentGameTime < GameController.instance.TotalDayTime / 2 && !day)
            ChangeClip();
    }

    private void ChangeClip()
    {
        if (day)
        {
            audioSource.clip = nightClip;
            day = false;
        }
        else
        {
            audioSource.clip = dayClip;
            day = true;
        }
        audioSource.Play();
    }
}
