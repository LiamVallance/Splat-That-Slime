using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image[] Fail_Counters;
    private int score = 0;
    private int fail_Counter = 0;

    GameController GameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void IncrementScore()
    {
        score += 10;
    }

    public void DecrementScore()
    {
        if (score > 0)
        {
            score -= 10;
        }
    }

    public void AddFailCounter()
    {
        if (fail_Counter < 3)
        {
            fail_Counter++;
            Fail_Counters[fail_Counter-1].enabled = true;
        }
        if (fail_Counter >= 3)
            GameOver();
    }

    private void GameOver()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>().GameOver();
    }
}
