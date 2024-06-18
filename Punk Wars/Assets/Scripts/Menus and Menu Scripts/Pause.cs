using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    
    //When user presses Esc checks if game is paused or not then runs the script
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused){
                Resume();
            }
            else{
                Paused();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false; 
    }

    void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true; 
    }

    // pauses game on crash
     public void PauseCrashed()
    {
        GamePaused = true; 
        Time.timeScale = 0f; 
    }

    //resumes game after crash
    public void ResumeCrashed() 
    {
        GamePaused = false; 
        Time.timeScale = 1f;
    }
}
