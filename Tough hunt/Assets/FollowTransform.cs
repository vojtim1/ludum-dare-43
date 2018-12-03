using UnityEngine;

public class FollowTransform : MonoBehaviour {
    public Vector2 offset;
    public GameObject follow;

	void Update () {
        transform.position = follow.transform.position + (Vector3)offset;
	}
}
