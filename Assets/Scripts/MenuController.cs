using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
}
