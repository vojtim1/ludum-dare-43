using UnityEngine;

public class SelfDestruction : MonoBehaviour {
    public float time;

	void Update () {
        time -= Time.deltaTime;
        if (time <= 0)
            Destroy(gameObject);
	}
}
