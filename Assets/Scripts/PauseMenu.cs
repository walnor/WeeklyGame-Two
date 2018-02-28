using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public void ResumeEvent()
    {
        Time.timeScale = 1.0f;

        gameObject.SetActive(false);
    }

    public void RestartEvent()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("Hello");
    }

    public void ExitEvent()
    {
        Application.Quit();
        print("Hello");
    }
}
