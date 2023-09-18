using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBounceNoCoin : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // Get references to the BoxCollider2D and PhysicsMaterial2D components
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
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
