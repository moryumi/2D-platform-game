using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("rain");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y<-0.7f)
        {
            gameObject.SetActive(false);
        }
    }

   
}
