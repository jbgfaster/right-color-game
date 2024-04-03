using System.Collections;
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
    private bool inGame;

    private Spawner spawnManager;


    void Start()
    {
        spawnManager = GameObject.FindObjectOfType<Spawner>();
        Physics.gravity = new Vector3(0, -20.0F, 0);
        titleScreen.SetActive(true);
        restartScreen.SetActive(false);
        helpScreen.SetActive(false);
        buttonStart.onClick.AddListener(StartGame);
        buttonRestart.onClick.AddListener(RestartGame);
    }

    public void ChangeDifficulty(int temp)
    {
        difficulty += temp;
        UpdateUI();
    }

    public bool InGame()
    {
        return inGame;
    }

    public void ChangeScore(int temp)
    {
        if(temp>0)
        {
            score += temp;
            if(score%10 == 0)
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
    }

    private void UpdateUI()
    {
        livesText.text = "LIVES : " + lives;
        count.text = "SCORE : " + score;
        difficultyText.text= "LEVEL : " + difficulty;        
    }

    private void StartGame()
    {
        helpScreen.SetActive(true);
        ClearScores();
        UpdateUI();
        StartCoroutine(ReStartCoroutine(3));
    }

    private void GameOver()
    {
        inGame = false;
        spawnManager.CancelInvoke();
        restartScreen.SetActive(true);

    }

    private void RestartGame()
    {
        ClearScores();
        UpdateUI();
        StartCoroutine(ReStartCoroutine(1));
    }

    private void ClearScores()
    {
        difficulty = 0;
        lives = 3;
        score = 0;
    }

    private IEnumerator ReStartCoroutine(float time)
    {       
        titleScreen.SetActive(false);        
        restartScreen.SetActive(false);
        yield return new WaitForSeconds(time);
        helpScreen.SetActive(false);
        inGame = true;
        spawnManager.Restart();
    }
}
