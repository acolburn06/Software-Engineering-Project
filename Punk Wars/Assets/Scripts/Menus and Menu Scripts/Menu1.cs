using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu1 : MonoBehaviour
{
    private SaveManager savemanager;
    int increment = 0;

    //Increases build index by 1 to go to the next scene in the index
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    //Sends you to the main menu or Scene 0
    public void Menu()
    {
        SceneManager.LoadScene(0);
        savemanager.Write("Scores", "Score", increment.ToString());
        increment++;
    }

    //Sends you to the Tutorial
    public void Tutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Closes App
    public void Quit()
    {
        savemanager.DeleteAndRecreateDatabase();
        Debug.Log("QUIT");
        Application.Quit();
    }
}
