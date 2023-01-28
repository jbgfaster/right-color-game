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
    
    private int difficulty = 0;
    private int score = 0;
    private int lives = 3;
    public bool inGame;

    private SpawnManager spawnManager;

    void Start()
    {
        spawnManager = GameObject.FindObjectOfType<SpawnManager>();
        Physics.gravity = new Vector3(0, -20.0F, 0);
        titleScreen.SetActive(true);
        restartScreen.SetActive(false);
        helpScreen.SetActive(false);
        buttonStart.onClick.AddListener(StartGame);
        buttonRestart.onClick.AddListener(RestartGame);
    }

    public void ChangeDifficulty(int temp)
    {
        difficulty+=temp;
        UpdateUI();
    }

    public void ChangeScore(int temp)
    {
        if(temp>0)
        {
            score += temp;
            if(score%10==0)
            {
                ChangeLives(1);
            }
        }        
        else
        {
            ChangeLives(-1);
        }
        UpdateUI();
    }

    private void ChangeLives(int temp)
    {
        lives += temp;
        if (lives <= 0)
        {
            GameOver();
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        livesText.text = "Lives : " + lives;
        count.text = "Score : " + score;
        difficultyText.text= "Difficulty : " + difficulty;        
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
        ClearScores();
        UpdateUI();
        StartCoroutine("StartCoroutine", 3);
    }

    private void RestartGame()
    {
        ClearScores();
        UpdateUI();
        StartCoroutine("StartCoroutine", 1);
    }

    private void ClearScores()
    {
        difficulty = 0;
        lives = 3;
        score = 0;
    }

    private IEnumerator StartCoroutine(float time)
    {       
        titleScreen.SetActive(false);        
        restartScreen.SetActive(false);
        yield return new WaitForSeconds(time);
        helpScreen.SetActive(false);
        inGame = true;
        spawnManager.Restart();
    }
}
