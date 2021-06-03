using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class movement : MonoBehaviour
{
    public Rigidbody2D rb;
    private PolygonCollider2D boxCollider;
    private bool collideWithGround;
    private bool dead, finish;
    public Camera gameCamera;
    public backgroundLoop refScript;
    private bool hey2 = true;
    public static movement Instance { get; private set; }
    [SerializeField]
    private int gameOverForce;
    [SerializeField]
    public float jetPackVelocity;
    private bool jetPack;
    public Animator fireAnimator;

    void Start()
    {
        jetPack = false;
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
       // Debug.Log("jetpack "+jetPack);
       // Debug.Log(transform.position);
        if (!finish)
        {
            if (!dead)
            {
                if (jetPack)
                {
                    if (Input.GetAxis("Vertical") > 0)
                    {
                        //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,20f),ForceMode2D.Force);
                        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * jetPackVelocity;
                        //transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")* Mathf.Lerp(10f, 20f, 1f), 0) * Time.deltaTime ;
                        Debug.Log("jetpack vertical");
                        //gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("vertical değil");
                        //gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    }
                    if (Input.GetAxis("Horizontal") > 0)
                    {
                        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * Mathf.Lerp(0, 3000, 0.001f);

                    }
                    if (Input.GetAxis("Horizontal") < 0)
                    {
                        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * Mathf.Lerp(0, 3000, 0.001f);
                    }
                    
                   
                    
                }
                else
                {
                    if (Input.GetAxis("Vertical") > 0)
                    {
                        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * Mathf.Lerp(0, 5000, 0.001f);
                    }
                    if (Input.GetAxis("Horizontal") > 0)
                    {
                        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * Mathf.Lerp(0, 7000, 0.001f);
                    }
                    if (Input.GetAxis("Horizontal") < 0)
                    {
                        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * Mathf.Lerp(0, 7000, 0.001f);
                    }
                }
             
                

                if (transform.position.x + 10 >= refScript.ScreenBounds.x / 2)
                {
                    gameCamera.transform.position = new Vector3(Mathf.Lerp(gameCamera.transform.position.x, transform.position.x, .5f), gameCamera.transform.position.y, gameCamera.transform.position.z);
                }
                if (transform.position.y >refScript.ScreenBounds.y)
                {
                    transform.position = new Vector2(transform.position.x ,refScript.ScreenBounds.y) ;
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
        else if (collision.gameObject.tag == "wheel_part")
        {
            gameObject.transform.SetParent(collision.transform);

        }
        else if (collision.gameObject.tag == "testere" || collision.gameObject.tag == "diken" || collision.gameObject.tag == "rain_drop")
        {
            dead = true;
            GameManager.Instance.GameOver();
            GameOverForce();
        }
        else if (collision.gameObject.tag == "door")
        {
            finish = true;
            GameManager.Instance.FinishLevel();
        }
        else if (collision.gameObject.tag == "jetpack")
        {
            jetPack = true;
            StartCoroutine("JetPackCountdown");
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.6f;
            collision.transform.GetComponent<PolygonCollider2D>().enabled = false;
            collision.transform.SetParent(gameObject.transform);
            collision.transform.localPosition = Vector3.zero;
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
        else if (collision.gameObject.tag == "wheel_part")
        {
            gameObject.transform.SetParent(null);
        }

    }
   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "rain")
        {
            GameManager.Instance.RainStart();
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
        else if(collision.gameObject.tag == "potion")
        {
            Destroy(collision.gameObject);
            Debug.Log("potion");
            GameManager.Instance.CheckPotionCount();
        }
    }
    public void GameOverForce()
    {
        rb.AddForce(new Vector2(-.5f,10) * Time.deltaTime * gameOverForce);
        boxCollider.isTrigger = true;
    }

    public void jetPackClosed()
    {
        jetPack = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 2f;
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).SetParent(gameObject.transform.parent);
    }

    IEnumerator JetPackCountdown()
    {
        fireAnimator.SetBool("isStart", true);
        yield return new WaitForSeconds(20f);
        fireAnimator.SetBool("isStart", false);
        fireAnimator.SetBool("isFinish", true);
        yield return new WaitForSeconds(5f);
        jetPackClosed();
    }
}
