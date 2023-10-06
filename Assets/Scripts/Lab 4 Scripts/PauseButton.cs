
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PauseButtonController : MonoBehaviour, IInteractiveButton
{
    private bool isPaused = false;
    public Sprite pauseIcon;
    public Sprite playIcon;
    private Image image;
    // Start is called before the first frame update
    public AudioSource gameSound;

    public UnityEvent gamePause;
    public UnityEvent gameResumed;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void ButtonClick()
    {
        Time.timeScale = isPaused ? 1.0f : 0.0f;
        isPaused = !isPaused;
        if (isPaused)
        {
            image.sprite = playIcon;
            gameSound.Pause();
            gamePause.Invoke();
        }
        else
        {
            image.sprite = pauseIcon;
            gameSound.Play();
            gameResumed.Invoke();
        }
    }
}
