using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Animation AddScoreText;
    [SerializeField] private Animation LoseScoreText;
    [SerializeField] private Animation FailText;
    
    [SerializeField] private Image[] Fail_Counters;
    public int score = 0;
    private int highScore;
    private int fail_Counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void IncrementScore()
    {
        AddScoreText.Play();
        score += 10;
    }

    public void DecrementScore()
    {
        if (score > 0)
        {
            LoseScoreText.Play();
            score -= 50;
            if (score < 0) // Do not allow score to decrement into minus values
                score = 0;
        }
    }

    // Adds fail counter when fail shape is hit until max then shows game over screen
    public void AddFailCounter()
    {
        if (fail_Counter < 3)
        {
            FailText.Play();
            fail_Counter++;
            Fail_Counters[fail_Counter-1].enabled = true;
        }
        if (fail_Counter >= 3)
            GameOver();
    }


    // Calls game over state in GameController script, also calls for the highscore to be checked
    private void GameOver()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>().GameOver();
        HighScore();
    }

    // Checks finished games score against the local stored highscore and updates if higher
    public void HighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = score.ToString();
        }
    }
}
