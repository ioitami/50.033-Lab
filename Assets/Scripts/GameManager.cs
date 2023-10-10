using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // events
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;

    public IntVariable gameScore;

    public GameConstants SMB;

    public GameObject mario;
    public GameObject fireflower;
    public GameObject starman;

    void Start()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
        // subscribe to scene manager scene change
        SceneManager.activeSceneChanged += SceneSetup;

        SMB.upSpeed = 30;
    }
    public void SceneSetup(Scene current, Scene next)
    {
        gameStart.Invoke();
        SetScore();
        SMB.upSpeed = 30;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        // reset score
        gameScore.Value = 0;
        SetScore();
   
        Time.timeScale = 1.0f;
        SMB.upSpeed = 30;

        mario.SetActive(false);
        mario.SetActive(true);
        fireflower.SetActive(true);
        starman.SetActive(true);
    }

    public void IncreaseScore(int increment)
    {
        gameScore.ApplyChange(increment);
        SetScore();
    }

    public void SetScore()
    {
        scoreChange.Invoke(gameScore.Value);
        Debug.Log("gamescore VALUE is :" + gameScore.Value);
    }


    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }
}