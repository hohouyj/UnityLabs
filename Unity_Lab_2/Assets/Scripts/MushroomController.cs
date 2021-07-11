using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private Rigidbody2D mushroomBody;
    private Vector2 currentPosition;
    private Vector2 currentDirection;
    public float speed;
    public float jumpSpeed;
    private bool touched = false;
    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        if (Random.value > 0.5){
            currentDirection = new Vector2(1,0);
        }else{
            currentDirection = new Vector2(-1,0);
        }
        currentPosition = mushroomBody.position;
        mushroomBody.AddForce(Vector2.up  *  20, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        mushroomBody.velocity = currentDirection*speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Obstacles"))
        {
            var chk = currentDirection.x;
            if (chk>0)
            {
                currentDirection = new Vector2(-1,0);
            }
            else if (chk<0)
            {
                currentDirection = new Vector2(1,0);
            }
            //direction = new Vector2(-direction.x, 0).normalized;
        }
        if (col.gameObject.CompareTag("Player"))
        {
            if (!touched)
            {
                touched = true;
                mushroomBody.velocity = Vector2.zero;
                currentDirection = Vector2.zero;
            }

        }
    }
}
