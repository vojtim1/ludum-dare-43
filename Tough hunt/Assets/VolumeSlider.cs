using UnityEngine.UI;
using UnityEngine;

public class VolumeSlider : MonoBehaviour {
    Slider slider;

    // Use this for initialization
    void Start() {
        slider = GetComponent<Slider>();
        slider.value = Settings.instance.GetVolume();
        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(delegate { SetVolume(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetVolume()
    {
        print("Slider");
        print(slider.value);
        Settings.instance.SetVolume(slider.value);
    }
}
