using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D marioBody;
    public float speed = 50f;
    public float maxSpeed = 65f;
    public float upSpeed = 30f;

    public float deathImpulse = 15f;
    private bool onGroundState = true;

    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public Camera Camgameover;

    public Animator marioAnimator;
    public Transform gameCamera;

    // for audio
    public AudioSource marioAudio;
    public AudioSource marioDeath;


    //public delegate void StompEnemyBelow(String name);
    //public event StompEnemyBelow StompBelow;
    private Camera camBG;

    [System.NonSerialized]
    public bool alive = true;
    

    void Awake(){
        // other instructions
        // subscribe to Game Restart event
        GameManager.instance.gameRestart.AddListener(GameRestart);
    }

    void Start()
    {
        marioSprite = GetComponent<SpriteRenderer>();

        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();

        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);

        // subscribe to scene manager scene change
        SceneManager.activeSceneChanged += SetStartingPosition;
    }

    public void SetStartingPosition(Scene current, Scene next)
    {
        if (next.name == "World 1-2")
        {
            Debug.Log("Change world 1-2");
            // change the position accordingly in your World-1-2 case
            this.transform.position = new Vector3(-6.5f, -0.5f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void FlipMarioSprite(int value)
    {
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.05f)
                marioAnimator.SetTrigger("onSkid");

        }

        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.05f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

    int collisionLayerMask = (1 << 6) | (1 << 7) | (1 << 8) | (1 << 10);
    void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
        {
            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    // FixedUpdate is called 50 times a second
    private bool moving = false;
    void FixedUpdate()
    {
        if (alive && moving)
        {
            Move(faceRightState == true ? 1 : -1);
        }
    }
    void Move(int value)
    {

        Vector2 movement = new Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (marioBody.velocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    private bool jumpedState = false;
    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);

        }
    }
    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;

        }
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive && other.transform.position.y >= transform.position.y - 0.1f)
        {
            Debug.Log("Killed by goomba!");
            Debug.Log(other.transform.position.y + "," + transform.position.y);
            // play death animation
            marioAnimator.Play("mario-die");
            marioAudio.PlayOneShot(marioDeath.clip);
            alive = false;
        }
        else if(other.gameObject.CompareTag("Enemy") && alive){
            Debug.Log("Stomped goomba:" + other.gameObject.name);
            //StompBelow?.Invoke(other.gameObject.name);
            GameManager.instance.IncreaseScore(1);
        }
    }

    public void GameRestart()
    {
        // reset position
        marioBody.transform.position = new Vector3(-6.5f, -0.5f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset camera position
        camBG = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camBG.backgroundColor = new Color(138f / 255f, 139f / 255f, 255f / 255f);
    }

    void PlayJumpSound()
    {
        // play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }
    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }
    void GameOverScene()
    {
        // stop time
        Time.timeScale = 0.0f;
        // set gameover scene
        GameManager.instance.GameOver();
        Camgameover.backgroundColor = Color.black;
        //scoreText.transform.localPosition = new Vector3(0, 0, 0);
    }
}
