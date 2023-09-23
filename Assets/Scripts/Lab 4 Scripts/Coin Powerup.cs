
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerup : BasePowerup
{
    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.Coin;
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
    }


    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: do something with the object
        GameManager manager;
        bool result = i.TryGetComponent<GameManager>(out manager);

        if(result){
            manager.IncreaseScore(1);
        }
    }
}