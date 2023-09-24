
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerup : BasePowerup
{
    // setup this object's type
    // instantiate variables
    private GameManager manager;
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.Coin;

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        Debug.Log("Coin Spawned");
        spawned = true;
        
        //play sound
        AudioSource source = this.GetComponent<AudioSource>();
        source.PlayOneShot(source.clip);

        this.GetComponent<Animator>().SetTrigger("spawned");
        //PowerupManager.instance.powerupCollected.Invoke(this);
        //ApplyPowerup(manager);
        spawned = false;
    }


    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: do something with the object
        manager.IncreaseScore(1);

    }
}