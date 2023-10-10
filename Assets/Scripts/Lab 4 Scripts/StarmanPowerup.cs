
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarmanPowerup : BasePowerup
{
    // setup this object's type
    // instantiate variables

    protected override void Start()
    {
        //base.Start(); // call base class Start()
        //this.type = PowerupType.FireFlower;
        //StartCoroutine(ActiveObject());
        Debug.Log("STAR IN SCENE");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("STAR COLIDED");

        if (col.gameObject.CompareTag("Player"))
        {
            // TODO: do something when colliding with Player

            // then destroy powerup (optional)
            gameObject.SetActive(false);

        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {

    }

    public void GameRestart()
    {
        Debug.Log("Game reset STARMAN");
        gameObject.SetActive(true);
        this.transform.position = new Vector3(-2f, 5f, 0f);

        //StartCoroutine(ActiveObject());
        //GameObject.Find("Fireflower").GetComponent<Animator>().SetBool("isSpawned", false);
    }

    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        //base.ApplyPowerup(i);
        // try
        MarioStateController mario;
        bool result = i.TryGetComponent<MarioStateController>(out mario);
        if (result)
        {
            mario.SetPowerup(this.powerupType);
        }
    }
}