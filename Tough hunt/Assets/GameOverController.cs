using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	public Text textField;
	public AudioClip playerDiedAudio;
	public AudioClip villageRaidedAudio;
	public void Start()
	{
		Settings settings = Settings.instance;

		int gameOverState = settings.GetGameOverState();

		string gameOverMessage = "";
		switch (gameOverState)
		{
			case 0:
				gameOverMessage = "You died. The game is over.";
				GetComponent<AudioSource>().PlayOneShot(playerDiedAudio);
				break;
			case 1:
				gameOverMessage = "The village was raided. The game is over.";
				GetComponent<AudioSource>().PlayOneShot(villageRaidedAudio);
				break;
			default:
				gameOverMessage = "Impossible message.";
				break;
		}
		textField.text = gameOverMessage;
	}

	public void ButtonPressed()
	{
		SceneManager.LoadScene(0);
	}
}
