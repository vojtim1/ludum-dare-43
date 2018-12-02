using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    Vector2 cameraStartPosition;
    Vector2 startPosition;
    Camera mainCamera;

    [SerializeField]
    float scale = 1;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
        startPosition = transform.position;
        cameraStartPosition = mainCamera.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = startPosition + ((Vector2)mainCamera.transform.position - cameraStartPosition) * scale;

	}
}
