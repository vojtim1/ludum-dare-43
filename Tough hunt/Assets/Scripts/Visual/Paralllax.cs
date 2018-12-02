using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralllax : MonoBehaviour {

    [SerializeField]
    private float scaling = 1;

    private Vector2 cameraPosition;

    private Vector2 cameraStartPosition;

    private Vector2 startPosition;

    private Camera camera;

	// Use this for initialization
	void Start () {
        camera = Camera.main;
        startPosition = transform.position;
        cameraStartPosition = camera.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = startPosition + ((Vector2)camera.transform.position - cameraStartPosition) * scaling;
	}
}
