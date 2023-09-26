using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour, IInteractiveButton
{
    public AudioSource gameSound;
    public void ButtonClick()
    {
        GameManager.instance.GameRestart();
        gameSound.Stop();
        gameSound.Play();
    }
}
