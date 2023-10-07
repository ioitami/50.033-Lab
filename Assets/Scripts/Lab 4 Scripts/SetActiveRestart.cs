using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveRestart : MonoBehaviour
{
    public GameObject powerup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRestart()
    {
        powerup.SetActive(true);

        powerup.GetComponent<MagicMushroomPowerup>().GameRestart();

    }

    IEnumerator ActiveObject()
    {
        yield return new WaitWhile(() => powerup.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static);
        powerup.GetComponent<Rigidbody2D>().simulated = true;
        powerup.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 3, ForceMode2D.Impulse); // move to the right
    }

}
