using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public  Rigidbody2D rigidBody;
    public  SpringJoint2D springJoint;
    public  GameObject consummablePrefab; // the spawned mushroom prefab
    public  SpriteRenderer spriteRenderer;
    public  Sprite usedQuestionBox; // the sprite that indicates empty box instead of a question mark
    private bool hit =  false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void  OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") &&  !hit){
            hit  =  true;
            // spawn the mushroom prefab slightly above the box
            Instantiate(consummablePrefab, new  Vector3(this.transform.position.x, this.transform.position.y  +  1.0f, this.transform.position.z), Quaternion.identity);
        }
    }
}
