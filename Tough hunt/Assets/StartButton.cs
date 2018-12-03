using UnityEngine.SceneManagement;
using UnityEngine;

public class StartButton : MonoBehaviour {

	public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
