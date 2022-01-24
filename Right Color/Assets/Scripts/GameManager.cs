using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour   
{
    [SerializeField] Text count;
    [SerializeField] Text difficultyText;
    [SerializeField] Text livesText;

    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject helpScreen;
    [SerializeField] GameObject restartScreen;

    [SerializeField] Button buttonStart;
    [SerializeField] Button buttonRestart;
    
    private SpawnManager spawnManager;

    public int difficulty = 0;
    public int balls = 0;
    public int lives = 3;
    public bool inGame;

    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        Physics.gravity = new Vector3(0, -20.0F, 0);
        titleScreen.SetActive(true);
        restartScreen.SetActive(false);
        helpScreen.SetActive(false);
        buttonStart.onClick.AddListener(StartGame);
        buttonRestart.onClick.AddListener(ReStartGame);
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + lives;
        count.text = "Score: " + balls;
        difficultyText.text= "Difficulty: " + difficulty;        
    }

    public void Balls(int temp)
    {
        if(balls>0 || temp>0)
        {
            balls += temp;
        }        
        if(temp<0)
        {
            Lives(-1);
        }
    }
    void Lives(int temp)
    {
        lives += temp;
        if (lives <= 0)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        inGame = false;
        spawnManager.CancelInvoke();
        restartScreen.SetActive(true);

    }
    void StartGame()
    {
        helpScreen.SetActive(true);
        StartCoroutine("StartCoroutine", 3);
    }
    void ReStartGame()
    {
        StartCoroutine("StartCoroutine", 1);
    }
    IEnumerator StartCoroutine(float time)
    {
        difficulty = 0;
        lives = 3;
        balls = 0;
        titleScreen.SetActive(false);        
        restartScreen.SetActive(false);
        yield return new WaitForSeconds(time);
        helpScreen.SetActive(false);
        inGame = true;
        spawnManager.Restart();
    }
}
