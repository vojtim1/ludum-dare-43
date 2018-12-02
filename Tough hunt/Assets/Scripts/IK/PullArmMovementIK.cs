using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullArmMovementIK : MonoBehaviour {
    [SerializeField]
    GameObject center;

    public static PullArmMovementIK instance;
    public float maxDistance = 1f;
    public float maxPull = 0.2f;

    float actualPull = 0;
    float pullMultiplier = 0;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetDirection = ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - center.transform.position)).normalized;
        targetDirection.z = 0;

        float maxMagnitude = maxDistance - maxPull * pullMultiplier;

        float multiplier = maxMagnitude / targetDirection.magnitude;

        if (targetDirection.magnitude != maxMagnitude)
            targetDirection *= multiplier;
        transform.position = center.transform.position + targetDirection;
    }

    public void SetPullMultiplier(float p)
    {
        pullMultiplier = p;
    }
}
