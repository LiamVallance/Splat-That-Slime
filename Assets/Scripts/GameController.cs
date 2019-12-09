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

    void TakeInput()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < 3; i++)
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
