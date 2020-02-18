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
        if (AdTimer.Instance != null)
            if(AdTimer.Instance.canShowAd) { return; }
                
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

    public void PlaySelectSound()
    {
        FindObjectOfType<AudioManager>().Play("Selection");
    }
}
