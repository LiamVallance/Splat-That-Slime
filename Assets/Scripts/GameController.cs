using UnityEngine;

public class GameController : MonoBehaviour
{
    Collider2D col;
    ScoreController ScM;
    SpawnController SpM;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Awake()
    {
        ScM = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreController>();
        SpM = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
        TakeInput();
    }

    // Takes players inputs on screen and handles players interaction with game assets/ gameplay
    void TakeInput()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Began)
                {
                    Collider2D touchCollider = Physics2D.OverlapPoint(touchPosition);
                    if (col == touchCollider)
                    {
                        if (col.tag == "Player")
                        {
                            ScM.IncrementScore();
                            SpM.speed++;
                            if (ScM.score > 400)
                                SpM.stage = 5;
                            else if (ScM.score > 300)
                                SpM.stage = 4;
                            else if (ScM.score > 200)
                                SpM.stage = 3;
                            else if (ScM.score > 100)
                                SpM.stage = 2;
                            else
                                SpM.stage = 1;
                        }
                        else if (col.tag == "Killer")
                            ScM.DecrementScore();
                        else
                            ScM.AddFailCounter();

                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    // Ensures assets are removed from memory once no longer required
    void CheckBounds()
    {
        if (transform.position.x <= -5) { Destroy(gameObject); }
        if (transform.position.x >= 5) { Destroy(gameObject); }
    }

    public void GameOver()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().IsGameOver();
    }
}
