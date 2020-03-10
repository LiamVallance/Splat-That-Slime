using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Collider2D col;
    ScoreController ScM;
    SpawnController SpM;
    AudioManager audioManager;

    [SerializeField] private GameObject Mute_btn;
    [SerializeField] private GameObject UnMute_btn;
    
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject AdContinue_btn;

    public string lastShape;

    private bool gamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        Time.timeScale = 1;
    }

    void Awake()
    {
        ScM = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreController>();
        SpM = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnController>();
        if (AudioListener.pause)
        {
            Mute_btn.SetActive(false);
            UnMute_btn.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamePaused)
        {
            TakeInput();
        }
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
                    
                    if(touchCollider == null) { return; }

                    lastShape = touchCollider.tag;
                    checkSlime(touchCollider.gameObject);
                }
            }
        }
    }

    private void checkSlime(GameObject slime)
    {
        if (slime.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled == false) { return; }
        
        Splat(slime);

        if (slime.tag == "S_Green")
            Score();
        else if (slime.tag == "S_Red")
            Lose();
        else
            Fail();
    }

    private void Score()
    {
        ScM.IncrementScore();
        SpM.speed++;
        if (ScM.score > 400)
            SpM.stage = 5;
        else if (ScM.score > 300)
            SpM.stage = 4;
        else
            SpM.stage = 3;
        if (lastShape == "S_Green")
            ScM.comboCounter++;
    }

    private void Lose()
    {
        ScM.DecrementScore();
        ScM.AddFailCounter();
        ScM.comboCounter = 0;
        ScM.comboBonus = 1;
    }

    private void Fail()
    {
        ScM.AddFailCounter();
        ScM.comboCounter = 0;
        ScM.comboBonus = 1;
    }

    private void Splat(GameObject slime)
    {
        slime.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        slime.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        GameObject.FindObjectOfType<AudioManager>().Play("Splat");
    }

    public void GameOver()
    {
        PauseGame();
        gameOverUI.SetActive(true);
    }

    public void AdPlayedContinue()
    {
        gameOverUI.SetActive(false);
        ScM.ResetFails();
        UnPauseGame();
        AdWatched();
    }

    private void AdWatched()
    {
        AdContinue_btn.SetActive(false);
    }

    private void AdReset()
    {
        AdContinue_btn.SetActive(true);
    }

    public void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
    }
}
