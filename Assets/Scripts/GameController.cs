using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Collider2D col;
    GameObject ScoreManager;
    ScoreController score;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
        score = (ScoreController)ScoreManager.GetComponent(typeof(ScoreController));
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
        CheckBounds();
    }

    void TakeInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchCollider = Physics2D.OverlapPoint(touchPosition);
                if (col == touchCollider)
                {
                    if (col.tag == "Player")
                        score.IncrementScore();
                    else
                        score.DecrementScore();

                    Destroy(gameObject);
                }
            }
        }
    }

    void CheckBounds()
    {
        if (transform.position.x <= -5) { Destroy(gameObject); }
        if (transform.position.x >= 5) { Destroy(gameObject); }
    }
}
