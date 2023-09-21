using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build.Content;
using Unity.VisualScripting;
using System;

public class EnemyMovement : MonoBehaviour
{
    public bool alive = true;

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;

    public Animator goombaAnimator;

    public AudioSource enemyAudio;


    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();

        goombaAnimator.SetBool("isDead", false);

        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.StompBelow += Die;
    }
    void ComputeVelocity()
    {
        velocity = new Vector2(moveRight * maxOffset / enemyPatroltime, 0);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if(goombaAnimator.GetBool("isDead") == false){
            if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
            {// move goomba
                Movegoomba();
            }
            else
            {
                // change direction
                moveRight *= -1;
                ComputeVelocity();
                Movegoomba();
            }
        }

    }
    public void GameRestart()
    {
        goombaAnimator.SetBool("isDead", false);
        goombaAnimator.SetTrigger("gameRestart");
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.gameObject.SetActive(true);

        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
    }

    private void Die(String nameObj){
        if(this.gameObject.name == nameObj){
            enemyAudio.PlayOneShot(enemyAudio.clip);
            goombaAnimator.SetBool("isDead", true);
            this.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(makeInvis(1f));
        }
    }

    IEnumerator makeInvis(float delay){
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
    }
}
