using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D marioBody;
    public float maxSpeed = 10;
    private float moveHorizontal;
    private float checkMove;
    public float upSpeed = 15;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private bool onGroundState = true;
    public int numberJumps = 1;
    
    public Transform enemyLocation;
    public Text scoreText;
    public int score = 0;
    private bool countScoreState = false;
    public bool dead = false;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    
    // Start is called before the first frame update
    void  Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      // toggle state
    //   if (Input.GetKeyDown("a") && faceRightState)
    //   {
    //     faceRightState = false;
    //     marioSprite.flipX = true;
    //     if (Mathf.Abs(marioBody.velocity.x) >  1.0){
    //         marioAnimator.SetTrigger("onSkid");
    //         }
    //   }

    //   if (Input.GetKeyDown("d") && !faceRightState){
    //     faceRightState = true;
    //     marioSprite.flipX = false;
    //     if (Mathf.Abs(marioBody.velocity.x) >  1.0){
    //         marioAnimator.SetTrigger("onSkid");
    //         }
    //   }
       
        // when jumping, and Gomba is near Mario and we haven't registered our score
    //   if (!onGroundState && countScoreState)
    //   {

    //     if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
    //     {
    //         countScoreState = false;
    //         score++;
    //         Debug.Log(score);
    //     }
        
    //   }
    //}
        moveHorizontal = Input.GetAxis("Horizontal");
        // dynamic rigidbody



        if (moveHorizontal<0 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if (moveHorizontal>0 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
        }
        
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d") || Input.GetKeyUp("left") || Input.GetKeyUp("right"))
        {
            if (Mathf.Abs(marioBody.velocity.x) > maxSpeed*0.75)// && Mathf.Abs(checkMove)>0)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }
        if (Mathf.Abs(moveHorizontal) == 0)
        {
            // stop
            //checkMove = Input.GetAxis("Horizontal");
            if (Mathf.Abs(marioBody.velocity.x) > maxSpeed*0.75)// && Mathf.Abs(checkMove)>0)
            {
                //marioAnimator.SetTrigger("onSkid");
            }
            marioBody.velocity = new Vector2(0,marioBody.velocity.y);
        }
        

        if (Input.GetKeyDown("space") && onGroundState){
          marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
          onGroundState = false;
          numberJumps--;
          countScoreState = true;
        }
        
        // if (Input.GetKeyDown("space") && numberJumps==2){
        //   marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
        //   onGroundState = false;
        //   numberJumps--;
        // }
        int layerMask = 1 << 3;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1000, layerMask);
        // If it hits something...
        if (hit.collider != null && countScoreState)
        {
            countScoreState = false;
            score++;
            Debug.Log(score);
            Debug.Log(hit.collider.tag);
        }

        marioAnimator.SetBool("onGround", onGroundState);
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    // called when the cube hits the floor


    void  FixedUpdate()
    {
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            movement.Normalize();
            if (marioBody.velocity.magnitude < maxSpeed){
                    marioBody.AddForce(movement * speed);
                }
        }

    }
    
    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Gomba!");
            OnDeath();
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacles"))
        {
            onGroundState = true; // back on ground
            countScoreState = false; // reset score state
            scoreText.text = "Score: " + score.ToString();
        };
    }
    
    void OnDeath()
    {
        dead = true;
        Time.timeScale = 0.0f;
    }

}
