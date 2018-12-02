using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {
    public Vector2 offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = (Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3)offset);
    }
}
