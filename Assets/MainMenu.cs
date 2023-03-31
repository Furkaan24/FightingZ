using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public void Playgame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void PlayMultiplayer()
    {
        SceneManager.LoadScene("Loading");
    }


    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
}
