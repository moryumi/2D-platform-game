using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundLoop : MonoBehaviour
{
    [SerializeField]
    private Vector2 screenBounds;
    private Camera mainCamera;
    public List<GameObject> sprites;
    public Vector2 ScreenBounds
    {
        get { return screenBounds; }
        set { screenBounds = value; }
    }

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,mainCamera.transform.position.z));
        //Debug.Log("screenbounds"+screenBounds);
    }

    void Update()
    {
        for (int i = 0; i < sprites.Count; i++)  //foreachte error verdi.looptayken listeden remove yaptığın için
        {
            repositionBackground(sprites[i]);
        }
    }

    public void repositionBackground(GameObject obj)
    {
        if (sprites.Count>1)
        {
            GameObject firstChild = sprites[0].gameObject;
            GameObject lastChild = sprites[sprites.Count-1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;  //last childın boundsı
            if (transform.position.x + screenBounds.x*1.5f > lastChild.transform.position.x + halfObjectWidth)//sağagidiyosa
            {
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2,lastChild.transform.position.y,lastChild.transform.position.z);
                sprites.Add(firstChild);
                sprites.RemoveAt(0);
            }
            else if (firstChild.transform.position.x-halfObjectWidth>transform.position.x-screenBounds.x)//sola gidiyosa
            {
                lastChild.transform.position = new Vector3(firstChild.transform.position.x-halfObjectWidth*2,firstChild.transform.position.y,firstChild.transform.position.z);
                sprites.Insert(0, lastChild); // add lastchild at 0.element
                sprites.RemoveAt(sprites.Count - 1);
            }
        }
    }
}
