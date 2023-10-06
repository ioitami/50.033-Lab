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
        gameRestart.Invoke();
        Time.timeScale = 1.0f;
        SMB.upSpeed = 30;
    }

    public void IncreaseScore(int increment)
    {
        gameScore.ApplyChange(1);
        SetScore();
    }

    public void SetScore()
    {
        scoreChange.Invoke(gameScore.Value);
    }


    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }
}