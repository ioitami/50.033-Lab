using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBounceNoCoin : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // Get references to the BoxCollider2D and PhysicsMaterial2D components
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.density = 1f;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
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

}
