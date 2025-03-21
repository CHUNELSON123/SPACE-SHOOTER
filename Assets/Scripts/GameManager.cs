using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    public bool _isGameOver;
    [SerializeField]
    private GameObject _resumeVisualizer;
    [SerializeField]
    private GameObject _pauseVisualizer;
    public bool isPaused = false;
    
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPaused)
            {
                Resumeplay();
            }
            else
            {
                PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PauseGame()
    {
        _pauseVisualizer.SetActive(false);
        _resumeVisualizer.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
     
    public void Resumeplay()
    {
        _resumeVisualizer.SetActive(false);
        _pauseVisualizer.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
    public void Replay()
    {
        SceneManager.LoadScene(1);
    }
}
