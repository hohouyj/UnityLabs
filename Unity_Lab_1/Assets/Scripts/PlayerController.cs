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
    
    // Start is called before the first frame update
    void  Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      // toggle state
      if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
      }

      if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;
      }
       
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
    }

    //marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));


    // called when the cube hits the floor


    void  FixedUpdate()
    {

        moveHorizontal = Input.GetAxis("Horizontal");
        // dynamic rigidbody
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed){
                    marioBody.AddForce(movement * speed);
                }
        }
        
        
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d") || Input.GetKeyUp("left") || Input.GetKeyUp("right")){
            // stop
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
        if (col.gameObject.CompareTag("Ground"))
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
