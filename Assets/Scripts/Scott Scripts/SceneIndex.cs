using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneIndex : MonoBehaviour
{
    private int sceneIndex;

    void start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
     public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void EndGame()
    {
        SceneManager.LoadScene(3);
    }
}
