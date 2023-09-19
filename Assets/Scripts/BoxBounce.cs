using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor.Build.Content;

public class BoxBounce : MonoBehaviour
{
    private Transform player;
    public Animator qnboxAnimator;
    public int initialNumCoins = 1;
    private int numCoins = 1;

    public GameObject coinPrefab;
    public GameManager gameManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        numCoins = initialNumCoins;
    }

    // Update is called once per frame
    void Update()
    {

        if (qnboxAnimator.GetBool("DisableBlock") == false && numCoins > 0)
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
        else
        {
            StartCoroutine(toStatic());
            //boxCollider.density = 1000f;
        }

    }
    IEnumerator toStatic()
    {
        yield return new WaitForSeconds(.5f);
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Player") && col.contacts[0].normal.y > 0 && numCoins > 0)
        {
            coinDecrease();
            spawnCoin();
        }
        if (numCoins <= 0)
        {
            qnboxAnimator.SetBool("DisableBlock", true);
        }
    }

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart QnBox!");
        qnboxAnimator.SetBool("DisableBlock", false);
        numCoins = initialNumCoins;
    }


    void coinDecrease()
    {
        numCoins -= 1;
    }

    void spawnCoin()
    {
        Instantiate(coinPrefab, this.transform);
        StartCoroutine(increaseScoreDelay(1.4f));
    }

    IEnumerator increaseScoreDelay(float delay){
        yield return new WaitForSeconds(delay);
        gameManager.IncreaseScore(1);
    }

}
