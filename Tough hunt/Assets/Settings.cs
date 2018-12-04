using UnityEngine;

public class Settings : MonoBehaviour {
    public static Settings instance;

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
        if (!PlayerPrefs.HasKey("volume"))
            PlayerPrefs.SetFloat("volume", 1);
        if (!PlayerPrefs.HasKey("showHints"))
            PlayerPrefs.SetInt("ShowHints", 1);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return PlayerPrefs.GetFloat("volume");
    }
    public void SetShowHints(int showHints)
    {
        PlayerPrefs.SetInt("showHints", showHints);
    }
    public bool GetShowHints()
    {
        if (PlayerPrefs.GetInt("showHints") != 0)
            return true;
        else return false;
    }
}
