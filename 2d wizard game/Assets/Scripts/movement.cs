using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class movement : MonoBehaviour
{
    public FixedJoystick fj;
    public Rigidbody2D rb;
    private PolygonCollider2D boxCollider;
    private bool collideWithGround;
    private bool dead, finish, enemyActive, jetPack, fanActive,onLadder,doorOpen,verticalStart,left,right,up,down;
    public bool Dead { get { return this.dead; } private set { } }
    public bool DoorOpen { get { return this.doorOpen; } set { this.doorOpen = value; } }
    public Camera gameCamera;
    public backgroundLoop refScript;
    private bool hey2 = true;
    public static movement Instance { get; private set; }
    [SerializeField]
    private int gameOverForce;
    [SerializeField]
    public float jetPackVelocity;
    public Animator fireAnimator;
    public Enemy enemy;
    public bool EnemyActive { get { return this.enemyActive; } private set { } }
    public GameObject port_A2;
    public GameObject port_B2;
    private RaycastHit2D raycastUp,raycastDown;
    public float distance, ladderSpeed;
    public LayerMask ladderLayer;
    public BoxCollider2D ladderParent;

    void Start()
    {
       // verticalStart = true;
        doorOpen = false;
        jetPack = false;
        fanActive = false;
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
        //raycastUp = Physics2D.Raycast(transform.position,Vector2.up,distance,ladderLayer);
        //raycastDown = Physics2D.Raycast(transform.position, Vector2.up, distance,ladderLayer);

        if (!finish)
        {
            if (!dead)
            {
               
                if (jetPack)
                {
                    if (left)
                    {
                        transform.position += new Vector3(-1, 1, 0) * Time.deltaTime * Mathf.Lerp(0, 3000, 0.001f);
                    }
                    if (right)
                    {
                        transform.position += new Vector3(1, 1, 0) * Time.deltaTime * Mathf.Lerp(0, 3000, 0.001f);
                    }
                    if (up)
                    {
                        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * jetPackVelocity;
                    }
                    else
                    {
                        Debug.Log("vertical değil");
                        
                    }

                    //if (Input.GetAxis("Vertical") > 0)
                    //{
                    //    //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,20f),ForceMode2D.Force);
                    //    //gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    //    //transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")* Mathf.Lerp(10f, 20f, 1f), 0) * Time.deltaTime ;
                    //    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * jetPackVelocity;
                    //    Debug.Log("jetpack vertical");
                    //}
                    //else
                    //{
                    //    Debug.Log("vertical değil");
                    //    //gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    //}
                    //if (Input.GetAxis("Horizontal") > 0)
                    //{
                    //    transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * Mathf.Lerp(0, 3000, 0.001f);

                    //}
                    //if (Input.GetAxis("Horizontal") < 0)
                    //{
                    //    transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * Mathf.Lerp(0, 3000, 0.001f);
                    //}
                }
                else
                {
                    if (!fanActive)
                    {
                        //Debug.Log("movement");
                        if (onLadder)
                        {
                            //if (Input.GetAxis("Horizontal") > 0)
                            //{
                            //    transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0) * Time.deltaTime * Mathf.Lerp(0, 7000, 0.001f);
                            //}
                            //if (Input.GetAxis("Horizontal") < 0)
                            //{
                            //    transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0) * Time.deltaTime * Mathf.Lerp(0, 7000, 0.001f);
                            //}

                            //if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
                            //{
                            //    Debug.Log("onLadder");
                            //    GetComponent<Rigidbody2D>().gravityScale = 0;
                            //    if (Input.GetAxis("Vertical") > 0)
                            //    {
                            //        ladderParent.isTrigger = true;
                            //        transform.position += new Vector3(0, 1f, 0) * Time.deltaTime * ladderSpeed;
                            //    }
                            //    if(Input.GetAxis("Vertical") < 0)
                            //    {
                            //        ladderParent.isTrigger = true;
                            //        transform.position -= new Vector3(0, 1f, 0) * Time.deltaTime * ladderSpeed;
                            //    }

                            //}
                            if (left)
                            {
                                transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * Mathf.Lerp(0, 7000, 0.001f);
                            }
                            if (right)
                            {
                                transform.position += new Vector3(1, 0, 0) * Time.deltaTime * Mathf.Lerp(0, 7000, 0.001f);
                            }
                            if (up || down)
                            {
                                GetComponent<Rigidbody2D>().gravityScale = 0;
                                if (up)
                                {
                                    ladderParent.isTrigger = true;
                                    transform.position += new Vector3(0, 1f, 0) * Time.deltaTime * ladderSpeed;
                                }
                                if (down)
                                {
                                    ladderParent.isTrigger = true;
                                    transform.position -= new Vector3(0, 1f, 0) * Time.deltaTime * ladderSpeed;
                                }

                            }
                            
                        }
                        else
                        {
                            //if (fj.Direction.y > 0)
                            //{
                            //    Debug.Log("up");
                            //    transform.position += new Vector3(0, fj.Direction.y, 0).normalized * Mathf.Lerp(0, 14, 0.8f * Time.deltaTime);
                            //}
                            //if (fj.Direction.x>0)
                            //{
                            //    Debug.Log("right");
                            //    transform.position += new Vector3(fj.Direction.x, 0, 0).normalized * Mathf.Lerp(0, 26, .3f * Time.deltaTime);// * Mathf.Lerp(0, 7000, 0.001f)
                            //}
                            //if (fj.Direction.x < 0)
                            //{
                            //    Debug.Log("left");
                            //    transform.position += new Vector3(fj.Direction.x, 0, 0).normalized * Mathf.Lerp(0, 26, .3f * Time.deltaTime);//* Mathf.Lerp(0, 7000, 0.001f)
                            //}
                            if (left)
                            {
                                Debug.Log("left");
                                transform.position += new Vector3(-1, 0, 0).normalized * Mathf.Lerp(0, 26, .3f * Time.deltaTime);//* Mathf.Lerp(0, 7000, 0.001f)
                            }
                            if (right)
                            {
                                Debug.Log("right");
                                transform.position += new Vector3(1, 0, 0).normalized * Mathf.Lerp(0, 26, .3f * Time.deltaTime);// * Mathf.Lerp(0, 7000, 0.001f)
                            }
                            if (up)
                            {
                                Debug.Log("up");
                                transform.position += new Vector3(0, 1, 0).normalized * Mathf.Lerp(0, 14, 0.8f * Time.deltaTime);
                            }
                            
                            GetComponent<Rigidbody2D>().gravityScale = 2;
                            #if (UNITY_EDITOR && !UNITY_ANDROID)
                                                        if (Input.GetAxis("Vertical") > 0)
                                                        {
                                                            transform.position += new Vector3(0, Input.GetAxis("Vertical"), 0).normalized * Mathf.Lerp(0, 14, 0.8f * Time.deltaTime);
                                                            // StartCoroutine("VerticalTime");
                                                        }
                                                        if (Input.GetAxis("Horizontal") > 0)
                                                        {
                                                            transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0).normalized * Mathf.Lerp(0, 26, .3f * Time.deltaTime);// * Mathf.Lerp(0, 7000, 0.001f)
                                                        }
                                                        if (Input.GetAxis("Horizontal") < 0)
                                                        {
                                                            transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0).normalized * Mathf.Lerp(0, 26, .3f * Time.deltaTime);//* Mathf.Lerp(0, 7000, 0.001f)
                                                        }
                            #endif

                        }

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
        else if (collision.gameObject.tag == "elevator")
        {
            gameObject.transform.SetParent(collision.transform);

        }
        else if (collision.gameObject.tag == "testere" || collision.gameObject.tag == "diken" || collision.gameObject.tag == "rain_drop")
        {
            dead = true;
            GameManager.Instance.GameOver();
            GameOverForce();
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
        else if (collision.gameObject.tag == "enemy")
        {
            dead = true;
            enemy.StopAnimation();
            GameOverForce();
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
        else if (collision.gameObject.tag == "elevator")
        {
            gameObject.transform.SetParent(null);
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
        else if (collision.gameObject.tag == "potion")
        {
            Destroy(collision.gameObject);
            Debug.Log("potion");
            GameManager.Instance.CheckPotionCount();
        }
        else if (collision.gameObject.tag == "port_A")
        {
            transform.localPosition = port_A2.transform.position;
        }
        else if (collision.gameObject.tag == "port_B")
        {
            transform.localPosition = port_B2.transform.position;
        }
        else if (collision.gameObject.tag == "fan")
        {
            fanActive = true;
        }
        else if (collision.gameObject.tag == "ladder")
        {
            onLadder = true;
            ladderParent = collision.transform.parent.GetComponent<BoxCollider2D>();
           //collision.transform.parent.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if (collision.gameObject.tag == "door")
        {
            if (doorOpen)
            {
                Debug.Log("dooropen");
                this.transform.GetChild(0).GetComponent<Animation>().Play();
                finish = true;
                GameManager.Instance.FinishLevel();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "rain")
        {
            GameManager.Instance.RainStart();
        }
        else if (collision.gameObject.tag == "enemy_zone")
        {
            enemyActive = true;
            enemy.FollowCharacter();
            enemy.AnimationPlay();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "rain")
        {
            GameManager.Instance.ExitRain();
        }
        else if (collision.gameObject.tag == "enemy_zone")
        {
            enemyActive = false;
            enemy.StopAnimation();
        }
        else if (collision.gameObject.tag == "fan")
        {
            fanActive = false;
        }
        else if (collision.gameObject.tag == "ladder")
        {
            ladderParent = collision.transform.parent.GetComponent<BoxCollider2D>();
            ladderParent.isTrigger = false;
            //collision.transform.parent.GetComponent<BoxCollider2D>().isTrigger = false;
            onLadder = false;
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
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).SetParent(gameObject.transform.parent);
    }

    IEnumerator JetPackCountdown()
    {
        fireAnimator.SetBool("isStart", true);
        yield return new WaitForSeconds(9f);
        fireAnimator.SetBool("isStart", false);
        fireAnimator.SetBool("isFinish", true);
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.8f;
        yield return new WaitForSeconds(5f);
        jetPackClosed();
    }
    
    public void Left()
    {
        
        left = true;
        right = false;
    }

    public void Right()
    {
        right = true;
        left = false;
    }

    public void Up()
    {
        up = true;
    }

    public void Down()
    {
        down = true;
    }

    public void Idle()
    {
        left = false;
        right = false;
        up = false;
    }

    public void IdleUp()
    {
        up = false;
    }

    //IEnumerator VerticalTime()
    //{
    //    yield return new WaitForSeconds(.4f);
    //    verticalStart = false;
    //    yield return new WaitForSeconds(.5f);
    //    verticalStart = true;
    //}
}
