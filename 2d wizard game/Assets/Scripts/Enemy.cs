using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public GameObject enemyZone;
    AnimationCurve curve;
    private bool right, left;

    // Start is called before the first frame update
    void Start()
    {
        left = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("dead "+movement.Instance.Dead);

        if (movement.Instance.EnemyActive)
        {
            if (transform.position.x <= movement.Instance.transform.position.x)
            {
                Debug.Log("rotate");
                transform.eulerAngles = new Vector3(0, 180, 0);

                //transform.Rotate(new Vector3(0,180,0));
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
            }
        }
        else
        {
            if (right)
            {
                transform.eulerAngles=new Vector3(0,180,0);
                transform.position += new Vector3(1f, 0, 0) * Time.deltaTime * speed;
            }
            else if (left)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.position += new Vector3(-1f, 0, 0) * Time.deltaTime * speed;
            }
            
            if (transform.position.x<2.83f)
            {
                //Debug.Log("right");
                right = true;
                left = false;
            }
            else if (transform.position.x > 21f)
            {
                left = true;
                right = false;
            }
        }
       
        
    }

    public void FollowCharacter()
    {
       // Debug.Log("followcharaacter");
        transform.position = Vector2.MoveTowards(transform.position,new Vector2(movement.Instance.transform.position.x, transform.position.y),Time.deltaTime*speed);
    }
    public void AnimationPlay()
    {
        GetComponent<Animation>().Play();
    }
    public void StopAnimation()
    {
        if (movement.Instance.Dead)
        {
            enemyZone.SetActive(false);
        }
        GetComponent<Animation>().Stop();
        
    }
}
