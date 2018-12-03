using UnityEngine.UI;
using UnityEngine;

public class SettingsToggle : MonoBehaviour {
    Toggle toggle;

	// Use this for initialization
	void Start () {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener(delegate { SetShowHints(); });
        toggle.isOn = Settings.instance.GetShowHints();
	}
	
	void SetShowHints()
    {
        int i = 0;
        if (toggle.isOn)
            i = 1;
        Settings.instance.SetShowHints(i);
    }
}
