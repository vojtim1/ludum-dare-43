using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hours : MonoBehaviour {
    public RectTransform yellowArm;

	// Use this for initialization
	void Start () {
        yellowArm.rotation = Quaternion.Euler(new Vector3(0,0,90));
	}
	
	// Update is called once per frame
	void Update () {
        float timeRatio = GameController.instance.CurrentGameTime / GameController.instance.TotalDayTime;
        yellowArm.rotation = Quaternion.Euler(new Vector3(0,0,90+-360*timeRatio));
	}
}
