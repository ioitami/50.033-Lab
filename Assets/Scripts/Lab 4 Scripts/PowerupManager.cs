using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public GameManager gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        gamemanager.gameRestart.AddListener(gameRestart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void gameRestart(){

    }
}
