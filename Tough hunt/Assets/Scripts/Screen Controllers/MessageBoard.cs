using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoard : MonoBehaviour {
	public Text textField;
	public Text titleField;
	public void DisplayText(string title, string toDisplay)
	{
		this.gameObject.SetActive(true);
		GameController.instance.PauseGame();
		titleField.text = title;
		textField.text = toDisplay;
	}

	public void ButtonClicked()
	{
		GameController.instance.UnPauseGame();
		this.gameObject.SetActive(false);
	}
}
