using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBounce : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Transform player;

    private void Start()
    {
        // Get references to the BoxCollider2D and PhysicsMaterial2D components
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.density = 1f;
    }

    // Update is called once per frame
    void Update()
    {

        if(player.transform.position.y >= this.transform.position.y){
            boxCollider.density = 1000f;
        }
        else{
            boxCollider.density = 1f;
        }
    }
}
