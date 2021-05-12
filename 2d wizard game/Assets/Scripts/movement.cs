using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class movement : MonoBehaviour
{

    public Rigidbody2D rb;
    private PolygonCollider2D boxCollider;
    private bool collideWithGround;
    private bool dead,finish;
    public Camera gameCamera;
    public backgroundLoop refScript;
    private bool hey2 = true;
    public static movement Instance { get; private set; }
    [SerializeField]
    private int gameOverForce;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        dead = false;
        finish = false;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<PolygonCollider2D>();
        boxCollider.isTrigger = false;
    }

    void Update()
    {
       // hey2=GameManager.Instance.Hey;
       // Debug.Log(hey2);

        if (!finish)
        {
            if (!dead)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * Mathf.Lerp(0, 7000, 0.001f);
                }
                if (Input.GetAxis("Horizontal") < 0)
                {
                    transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * Mathf.Lerp(0, 7000, 0.001f);
                }
                if (Input.GetAxis("Vertical") > 0)
                {
                    transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * Mathf.Lerp(0, 5000, 0.001f);
                }

                if (transform.position.x + 10 >= refScript.ScreenBounds.x / 2)
                {
                    gameCamera.transform.position = new Vector3(Mathf.Lerp(gameCamera.transform.position.x, transform.position.x, .5f), gameCamera.transform.position.y, gameCamera.transform.position.z);
                }
            }
        }
        if (transform.position.y < -13f)
        {
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "collider")
        {
           // Debug.Log("collideEnter");
            collideWithGround = true;
        }
        else if (collision.gameObject.tag == "moveablecollider")
        {
           // Debug.Log("moveable");
            gameObject.transform.SetParent(collision.transform.parent);
            
        }
        else if (collision.gameObject.tag == "testere" || collision.gameObject.tag == "diken" || collision.gameObject.tag == "rain_drop")
        {
            dead = true;
            GameManager.Instance.GameOver();
            GameOverForce();
        }
        else if (collision.gameObject.tag == "door_open")
        {
            collision.transform.GetChild(0).gameObject.SetActive(false);
            collision.transform.GetChild(1).gameObject.SetActive(true);
            finish = true;
            GameManager.Instance.FinishLevel(collision.gameObject);
        }
       

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "collider")
        {
            //Debug.Log("collideExit");
            collideWithGround = false;
        }
        else if (collision.gameObject.tag == "moveablecollider")
        {
            //Debug.Log("moveable");
            gameObject.transform.SetParent(null);
        }
        
    }
   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "rain")
        {
            GameManager.Instance.Rain();
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "rain")
        {
            GameManager.Instance.ExitRain();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "rain_drop")
        {
            dead = true;
            GameManager.Instance.GameOver();
            GameOverForce();
        }
    }
    public void GameOverForce()
    {
        rb.AddForce(new Vector2(-.5f,10) * Time.deltaTime * gameOverForce);
        boxCollider.isTrigger = true;
    }
}
