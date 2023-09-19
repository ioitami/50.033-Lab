using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinBounce : MonoBehaviour
{
    public Rigidbody2D coinBody;
    public float upSpeed = 1f;
    public float lifetime = 1f;

    public AudioSource coinAudio;
    public AudioClip coinGet;

    // Start is called before the first frame update
    void Start()
    {
        coinBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
        StartCoroutine(playSoundWithDelay(coinGet, lifetime));
        Destroy(this.gameObject, lifetime + 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator playSoundWithDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        coinAudio.PlayOneShot(clip);
        this.gameObject.GetComponent<Renderer>().enabled = false;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
