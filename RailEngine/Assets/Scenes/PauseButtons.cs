using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour {

    public void quitButton()
    {
        Application.Quit();
        Debug.Log("Quit Button");
    }
    public void restartButton()
    {
        SceneManager.LoadScene("LevelDemo");
        Debug.Log("Restart Button");
    }
}
