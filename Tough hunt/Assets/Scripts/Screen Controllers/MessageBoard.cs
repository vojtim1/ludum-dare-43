using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoard : MonoBehaviour {
	public Text textField;
	public Button okButton;

	public void DisplayText(string toDisplay)
	{
		GameController.instance.PauseGame();
		textField.text = toDisplay;
	}

	public void ButtonClicked()
	{

	}
}
