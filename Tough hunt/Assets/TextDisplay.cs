using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {

	public string toDisplay;
	public Text textField;

	private void Start()
	{
		SetText(0);
	}
	public void SetText(int amount)
	{
		textField.text = toDisplay + ": " + amount;
	}
}
