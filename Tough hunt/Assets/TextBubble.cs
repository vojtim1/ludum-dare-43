using UnityEngine.UI;
using UnityEngine;

public class TextBubble : MonoBehaviour {
    public static TextBubble instance;
    public GameObject prefab;

    private void Start()
    {
        instance = this;
    }

    public void Say(string text, GameObject go, float time)
    {
        GameObject canvas = Instantiate(prefab);
        canvas.GetComponent<FollowTransform>().follow = go;
        canvas.GetComponentInChildren<Text>().text = text;
        canvas.GetComponent<SelfDestruction>().time = time;
    }
}
