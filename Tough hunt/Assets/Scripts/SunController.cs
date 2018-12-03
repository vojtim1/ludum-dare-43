using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour {

	Vector2 cameraStartPosition;
	Vector2 startPosition;
	Camera mainCamera;

	float camHeight;
	float camWidth;

	[SerializeField]
	float shift = 1;

	void Start () {
		mainCamera = Camera.main;
		cameraStartPosition = mainCamera.transform.position;

		camHeight = 2f * mainCamera.orthographicSize;
		camWidth = camHeight * mainCamera.aspect;

		startPosition = cameraStartPosition + new Vector2(-camWidth * shift, 0);
	}
	
	void Update () {
		float timeFraction = GameController.instance.CurrentGameTime / GameController.instance.TotalDayTime;
		float yPosition = timeFraction;
		float xPosition = timeFraction;
		if (timeFraction > 0.5)
		{
			GetComponent<SpriteRenderer>().enabled = false;
		} else
		{
			GetComponent<SpriteRenderer>().enabled = true;
		}
		yPosition = Mathf.Sin(2 * Mathf.PI * yPosition);

		Vector2 newPosition = new Vector2(startPosition.x + 4 * shift * xPosition * camWidth, startPosition.y + yPosition * camHeight / 2);

		transform.position = newPosition + ((Vector2)mainCamera.transform.position - cameraStartPosition);
	}
}
