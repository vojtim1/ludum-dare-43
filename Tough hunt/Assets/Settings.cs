using UnityEngine;

public class Settings : MonoBehaviour {
    public static Settings instance;

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
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
	public void SetGameOverState(GameOverState state)
	{
		switch (state)
		{
			case GameOverState.PLAYERDIED:
				PlayerPrefs.SetInt("gameOverState", 0);
				break;
			case GameOverState.VILLAGERAIDED:
				PlayerPrefs.SetInt("gameOverState", 1);
				break;
		}
	}
	public int GetGameOverState()
	{
		return PlayerPrefs.GetInt("gameOverState");
	}

	public void SetDaysSurvived(int daysSurvived)
	{
		PlayerPrefs.SetInt("daysSurvived", daysSurvived);
	}
	public int GetDaysSurvived()
	{
		return PlayerPrefs.GetInt("daysSurvived");
	}
}