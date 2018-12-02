using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CameraMovementControl : MonoBehaviour
{
	public float cameraSpeed = 5F;
	public float horizontalBorder = 0.12F;
	public float verticalBorder = 0.12F;
	public float maxZoom = 20;
	public float minZoom = 1;

	Transform player;
	Transform cam;

	void Start()
	{
		cam = transform;
		Camera.main.orthographicSize = Mathf.Min(minZoom, maxZoom);
	}

	void LateUpdate()
	{
		if (!GameController.instance.GamePaused)
		{
			if (player == null) player = GameObject.FindWithTag("Player").GetComponent<Transform>();

			Vector2 mousePos = Input.mousePosition;

			if (mousePos.x < Screen.width * horizontalBorder) mousePos.x = Screen.width * horizontalBorder;
			if (mousePos.x > Screen.width - Screen.width * horizontalBorder) mousePos.x = Screen.width - Screen.width * horizontalBorder;

			if (mousePos.y < Screen.height * verticalBorder) mousePos.y = Screen.height * verticalBorder;
			if (mousePos.y > Screen.height - Screen.height * verticalBorder) mousePos.y = Screen.height - Screen.height * verticalBorder;

			mousePos = Camera.main.ScreenToWorldPoint(mousePos);

			Vector2 tPos = new Vector2((player.position.x + mousePos.x) / 2, (player.position.y + mousePos.y) / 2);

			Vector2 direction = new Vector2(tPos.x - cam.position.x, tPos.y - cam.position.y);

			cam.position = new Vector3(cam.position.x + direction.x * Time.deltaTime * cameraSpeed, cam.position.y + direction.y * Time.deltaTime * cameraSpeed, cam.position.z);

			if (Input.GetAxis("Mouse ScrollWheel") > 0)
			{
				Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize - 1, minZoom);
			}
			if (Input.GetAxis("Mouse ScrollWheel") < 0)
			{
				Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize + 1, maxZoom);
			}
		}
	}
}