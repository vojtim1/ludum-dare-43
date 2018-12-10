using UnityEngine;

public class AudioSourceController : MonoBehaviour {
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        float volume = audioSource.volume * Settings.instance.GetVolume();
        audioSource.volume = volume;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
