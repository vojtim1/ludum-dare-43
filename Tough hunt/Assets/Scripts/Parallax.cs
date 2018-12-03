using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    Vector2 cameraStartPosition;
    Vector2 startPosition;
    Camera mainCamera;

    [SerializeField]
    float scale = 1;

	[SerializeField]
	Vector2 offset;
	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
        cameraStartPosition = mainCamera.transform.position;
		startPosition = cameraStartPosition + offset;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = startPosition + ((Vector2)mainCamera.transform.position - cameraStartPosition) * scale;

	}
}
