
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicMushroomPowerup : BasePowerup
{
    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.MagicMushroom;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.GetComponent<Rigidbody2D>().simulated = false;
        StartCoroutine(ActiveObject());

        GameManager.instance.gameRestart.AddListener(GameRestart);
    }

    IEnumerator ActiveObject()
    {
        yield return new WaitWhile(() => this.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static);
        this.GetComponent<Rigidbody2D>().simulated = true;
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse); // move to the right
    }
        void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            // TODO: do something when colliding with Player

            // then destroy powerup (optional)
            gameObject.SetActive(false);

        }
        else if (col.gameObject.layer == 10) // else if hitting Pipe, flip travel direction
        {
            if (spawned)
            {
                goRight = !goRight;
                rigidBody.AddForce(Vector2.right * 3 * (goRight ? 1 : -1), ForceMode2D.Impulse);

            }
        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        GameObject.Find("Mushroom").GetComponent<Animator>().SetBool("isSpawned", true);
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Debug.Log("push mush");
        spawned = true;
    }

    public void GameRestart()
    {
        gameObject.SetActive(true);
        GameObject.Find("Mushroom").GetComponent<Animator>().SetBool("isSpawned", false);
        this.transform.localPosition = new Vector3(0f, 0f, 0f);
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.GetComponent<Rigidbody2D>().simulated = false;
        spawned = false;

        StartCoroutine(ActiveObject());
        GameObject.Find("Mushroom").GetComponent<Animator>().SetBool("isSpawned", false);
    }

    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: do something with the object

    }
}