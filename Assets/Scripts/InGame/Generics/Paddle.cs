using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public static bool isPlayerBot;
    private float speed = 20f;
    public Rigidbody2D rb;
    public Vector3 startPosition;
    private bool canMove1 = true, canMove2 = true;

    // private float speedModifier = 0.12f;
    // private float dragModifier = 0.9f;
    private float deadzone = 3.5f;

    private AudioSource source;
    //private float velToVol = 1f;

    private float movement;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerBot = StaticGameInfo.getPlayerBot();
        startPosition = transform.position;

    }

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int touches = 0;

        if (GameManager.countDone)
        {

            foreach (Touch touch in Input.touches)
            {
                Vector3 relativePos = Camera.main.ScreenToWorldPoint(touch.position);
                //Vector2 vel = new Vector2(0,0);
                if (relativePos.y < 0 && isPlayer1)
                {
                    // if(touch.phase != TouchPhase.Moved ){
                    //     rb.velocity = new Vector2(0, 0);
                    // }else{
                    // if(relativePos.x > transform.position.x){
                    //     rb.velocity = new Vector2(12f, 0);
                    // }else{
                    //     rb.velocity = new Vector2(-12f, 0);
                    // }
                    if (canMove1 || Mathf.Abs(transform.position.x) > Mathf.Abs(relativePos.x))
                    {
                        transform.position = new Vector3(relativePos.x, transform.position.y, transform.position.z);
                    }


                    //rb.velocity = new Vector2(touch.deltaPosition.x/touch.deltaTime * speedModifier, 0);
                    // } 
                    //print(rb.velocity);
                }
                else if (relativePos.y > 0 && !isPlayer1 && !isPlayerBot)
                {
                    // if(touch.phase != TouchPhase.Moved ){
                    //     rb.velocity = new Vector2(0, 0);
                    // }else{
                    // if(relativePos.x > transform.position.x){
                    //     rb.velocity = new Vector2(12f, 0);
                    // }else{
                    //     rb.velocity = new Vector2(-12f, 0);
                    // }
                    if (canMove2 || Mathf.Abs(transform.position.x) > Mathf.Abs(relativePos.x))
                    {
                        transform.position = new Vector3(relativePos.x, transform.position.y, transform.position.z);
                    }
                    //rb.velocity = new Vector2(touch.deltaPosition.x/touch.deltaTime * speedModifier, 0);
                    // } 
                    //print(rb.velocity);
                }
                touches++;
            }
            // if(touches == 0){
            if (isPlayer1)
            {
                movement = Input.GetAxisRaw("Horizontal");
            }
            else if (!isPlayerBot)
            {
                movement = Input.GetAxisRaw("Horizontal2");
            }
            else
            {
                float pongX = GameObject.Find("Pong").transform.position.x;
                if (pongX > transform.position.x)
                {
                    movement = 1;
                }
                else
                {
                    movement = -1;
                }
                if (Mathf.Abs(pongX - transform.position.x) < 2)
                {
                    movement = 0;
                }
            }
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
            // }
            // if(rb.velocity.x > 0){
            // rb.AddForce(new Vector3(-speed, 0, 0));
            // }else if(rb.velocity.x < 0){
            // rb.AddForce(new Vector3(speed, 0, 0));
            // }
            if (Mathf.Abs(deadzone - Mathf.Abs(rb.velocity.x)) < deadzone)
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayer1)
        {
            canMove1 = false;
        }
        else
        {
            canMove2 = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPlayer1)
        {
            canMove1 = true;
        }
        else
        {
            canMove2 = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            AudioClip sound = Resources.Load("AudioClips/pingPongSlaps/pingPongSlap_" + Random.Range(1, 9)) as AudioClip;

            // float hitVol = other.relativeVelocity.magnitude * velToVol;
            // print(sound);
            // print(Resources.Load("AudioClips/pingPongHit_01"));

            source.clip = sound;
            source.Play();
        }

    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
