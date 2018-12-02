using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoard : MonoBehaviour {
	public Text textField;

	public void Start()
	{
		this.gameObject.SetActive(false);
	}

	public void DisplayText(string toDisplay)
	{
		this.gameObject.SetActive(true);
		GameController.instance.PauseGame();
		textField.text = toDisplay;
	}

	public void ButtonClicked()
	{
		GameController.instance.UnPauseGame();
		this.gameObject.SetActive(false);
	}
}
