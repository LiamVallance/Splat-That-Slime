using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

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
}
