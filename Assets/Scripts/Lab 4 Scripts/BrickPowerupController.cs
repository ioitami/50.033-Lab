using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;
    public BasePowerup powerup; // reference to this question box's powerup
    public bool isBreakable = false;
    
    public int initialNumCoins = 1;
    private int numCoins = 1;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        numCoins = initialNumCoins;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Animator>().GetBool("DisableBlock") == false)
        {
            // if player above box
            if (player.transform.position.y - 0.5f >= this.transform.position.y)
            {
                this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
            else
            {
                this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

        private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && other.contacts[0].normal.y > 0 ){
            if (!powerup.hasSpawned){

                // spawn the powerup
                powerupAnimator.SetTrigger("spawned");

                Debug.Log("COIN SPAWNED");

                if(!isBreakable){
                    // set disabled if not breakable
                    this.GetComponent<Animator>().SetTrigger("spawned");
                    this.GetComponent<Animator>().SetBool("DisableBlock", true);
                }
            }
            else{
                if(isBreakable){

                }
            } 

        }
    }

    public void RestartButtonCallback(int input)
    {
        this.GetComponent<Animator>().SetBool("DisableBlock", false);
        numCoins = initialNumCoins;
    }

    void coinDecrease()
    {
        numCoins -= 1;
    }


    // used by animator
    public void Disable()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }
}
