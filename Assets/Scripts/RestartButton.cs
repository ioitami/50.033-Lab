using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RestartButton : MonoBehaviour, IInteractiveButton
{
    public AudioSource gameSound;
    public UnityEvent gameRestart;
    public void ButtonClick()
    {
        gameRestart.Invoke();
        gameSound.Stop();
        gameSound.Play();
    }
}
