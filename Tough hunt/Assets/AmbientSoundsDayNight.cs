using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundsDayNight : MonoBehaviour {
    public AudioClip dayClip;
    [Range(0,1)]
    public float dayVolume = 1;
    public AudioClip nightClip;
    [Range(0, 1)]
    public float nightVolume = 1;

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
        //if (GameController.instance.CurrentGameTime + 2 > GameController.instance.TotalDayTime / 2 || GameController.instance.CurrentGameTime + 2 > GameController.instance.TotalDayTime)
        //{
        //    StopCoroutine(FadeOut());
        //    StopCoroutine(FadeIn());
        //    StartCoroutine(FadeOut());
        //}
    }

    private void ChangeClip()
    {
        if (day)
        {
            audioSource.clip = nightClip;
            audioSource.volume = nightVolume;
            day = false;
        }
        else
        {
            audioSource.clip = dayClip;
            audioSource.volume = dayVolume;
            day = true;
        }
        audioSource.Play();
        UpdateVolume();
    }

    void UpdateVolume()
    {
        audioSource.volume *= Settings.instance.GetVolume();
    }

    //IEnumerator FadeOut()
    //{
    //    for (float f = audioSource.volume; f > 0; f -= .5f * Time.deltaTime)
    //    {
    //        audioSource.volume = f;
    //        yield return null;
    //    }

    //    StartCoroutine(FadeIn());
    //    StopCoroutine(FadeOut());
    //}

    //IEnumerator FadeIn()
    //{
    //    for (float f = audioSource.volume; audioSource.volume < 1; f += .5f * Time.deltaTime)
    //    {
    //        audioSource.volume = f;
    //        yield return null;
    //    }
    //    StopCoroutine(FadeIn());
    //}
}
