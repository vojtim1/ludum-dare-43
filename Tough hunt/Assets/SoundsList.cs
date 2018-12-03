using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundsList {
    public string listName;
    [Range(0,1)]
    public float volume;
    public List<AudioClip> audioClips;

    public AudioClip GetRandomClip()
    {
        int random = Random.Range(0, audioClips.Count);
        return audioClips[random];
    }
    public float GetVolume()
    {
        return volume;
    }
}
