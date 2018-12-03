using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoard : MonoBehaviour {
	public Button closeButton;
	public Text textField;
	public Text titleField;
	public void DisplayText(string title, string toDisplay, bool closable)
	{
		this.gameObject.SetActive(true);
		if (closable)
		{
			closeButton.gameObject.SetActive(true);
		} else
		{
			closeButton.gameObject.SetActive(false);
		}
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
