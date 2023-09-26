using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine.Audio;

public class HUDManager : MonoBehaviour
{
    private Vector3[] scoreTextPosition = {
        new Vector3(-640, 391, 0),
        new Vector3(0, 0, 0)
        };
    private Vector3[] restartButtonPosition = {
        new Vector3(824, 424, 0),
        new Vector3(0, -150, 0)
    };

    public GameObject highscoreText;
    public IntVariable gameScore;

    public GameObject scoreText;
    public Transform restartButton;
    public GameObject backText;

    public GameObject gameoverText;
    private AudioMixerSnapshot snapshot;
    public AudioMixer mixer;
    public GameObject pausebtn;

    void Awake()
    {
        // other instructions
        // subscribe to events
        GameManager.instance.gameStart.AddListener(GameStart);
        GameManager.instance.gameOver.AddListener(GameOver);
        GameManager.instance.gameRestart.AddListener(GameStart);
        GameManager.instance.scoreChange.AddListener(SetScore);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        // hide gameover text
        gameoverText.SetActive(false);
        highscoreText.SetActive(false);
        pausebtn.SetActive(true);
        backText.SetActive(false);
        scoreText.transform.localPosition = scoreTextPosition[0];
        restartButton.localPosition = restartButtonPosition[0];

        gameScore.SetValue(0);

        snapshot = mixer.FindSnapshot("Default");
        snapshot.TransitionTo(0.01f); //transition to snapshot
    }

    public void SetScore(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        Debug.Log("score is updated!");
    }


    public void GameOver()
    {
        gameoverText.SetActive(true);
        pausebtn.SetActive(false);
        backText.SetActive(true);
        scoreText.transform.localPosition = scoreTextPosition[1];
        restartButton.localPosition = restartButtonPosition[1];

        snapshot = mixer.FindSnapshot("Death");
        snapshot.TransitionTo(0.01f); //transition to snapshot

        // set highscore
        highscoreText.GetComponent<TextMeshProUGUI>().text = "TOP- " + gameScore.previousHighestValue.ToString("D6");
        // show
        highscoreText.SetActive(true);
    }
}
