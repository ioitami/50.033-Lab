using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;
    public BasePowerup powerup; // reference to this question box's powerup

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        if (other.gameObject.tag == "Player" && !powerup.hasSpawned && other.contacts[0].normal.y > 0){
            // show disabled sprite
            this.GetComponent<Animator>().SetBool("DisableBlock", true);
            // spawn the powerup
            powerupAnimator.SetTrigger("spawned");

            Debug.Log("MUSHROOM SPAWNED");
        }
    }

    // used by animator
    public void Disable()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }



}