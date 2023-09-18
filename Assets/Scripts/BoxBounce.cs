using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor.Build.Content;

public class BoxBounce : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    private Transform player;
    public Animator qnboxAnimator;
    public int initialNumCoins = 1;
    private int numCoins = 1;

    public GameObject coinPrefab;

    private bool belowBox;

    private void Start()
    {
        // Get references to the BoxCollider2D and PhysicsMaterial2D components
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.density = 1f;

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
                boxCollider.density = 1000f;
            }
            else
            {
                boxCollider.density = 1f;
            }
        }
        else
        {
            boxCollider.density = 1000f;
        }

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
    }

}
