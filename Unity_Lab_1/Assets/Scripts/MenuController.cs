using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public Transform prefab;
    public Text scoreTextUI;
    private int score1;
    private GameObject mario1;
    private bool marioDead = false;
    private GameObject[] enemylist;
    public Text buttonText;
    private bool ispause = true;


    void Awake()
    {
        Debug.Log("UI Awake");
        spawnMario();
        Time.timeScale = 0.0f;
        ispause = true;
    }
    // Start is called before the first frame update

    void resetEnemy()
    {
        marioDead = false;
        ispause = false;
        Time.timeScale = 0.0f;
        buttonText.text = "Restart";
        Debug.Log("UI resetEnemy");
        scoreTextUI.text = "Score: " + 0;
        //yield WaitForSeconds(5.0);  // or however long you want it to wait
        Application.LoadLevel(Application.loadedLevel);
        // enemylist = GameObject.FindGameObjectsWithTag("Enemy");
        // int i = 0;
        // foreach(GameObject e in enemylist)
        // {
        //     var enemypos = e.GetComponent<Transform>().position;
        //     if (enemypos.x != new Vector2(5,-3.5f).x || enemypos.x != new Vector2(-5,-3.5f).x)
        //     {
        //         e.GetComponent<Transform>().position = new Vector2(5-10*i,-3.5f);
        //         i++;
        //     }
        // }
        // spawnMario();
    }

    void spawnMario()
    {
        // if(GameObject.Find("Mario(Clone)")!=null){
        //     Destroy(GameObject.Find("Mario(Clone)"));
        // }
        // marioDead = false;
        // Instantiate(prefab, new Vector3(0,-3,0), Quaternion.identity);
    }
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(marioDead)
        {
            ispause = true;
            marioDead = false;
            Time.timeScale = 0.0f;
            Debug.Log("Mario IS DEAD");
            foreach (Transform eachChild in transform)
            {
                if (eachChild.name != "Score")
                {
                    Debug.Log("Child found. Name: " + eachChild.name);
                    // disable them
                    eachChild.gameObject.SetActive(true);
                }
            }
            resetEnemy();
        }

        if(GameObject.Find("Mario")!=null && !ispause)
        {
            mario1 = GameObject.FindGameObjectsWithTag("Player")[0];
            score1 = mario1.GetComponent<PlayerController>().score;
            marioDead = mario1.GetComponent<PlayerController>().dead;
            //scoreTextUI.text = "Score: " + score1.ToString();
        }
    }

    public void StartButtonClicked()
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name != "Score")
            {
                Debug.Log("Child found. Name: " + eachChild.name);
                // disable them
                eachChild.gameObject.SetActive(false);
                
            }
        }
        Time.timeScale = 1.0f;
        ispause = false;

    }
}
