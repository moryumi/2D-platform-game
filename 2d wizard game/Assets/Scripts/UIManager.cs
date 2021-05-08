using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UIManager : MonoBehaviour
{
    public List<Sprite> spriteList;
    public Button sampleButton;
    public List<Button> buttonList;
    [SerializeField]
    private int distance_x, distance_y=0;
    private Button firstButton;
    // Start is called before the first frame update
    void Awake()
    {
        firstButton = sampleButton;
        buttonList.Add(firstButton);
        firstButton.onClick.AddListener(() => LevelButtonClick(firstButton));
        for (int y = 0; y < 3; y++)
        {
            for (int z = 0; z < 5; z++)
            {
                if (z == 0 && y==0)
                {

                }
                else
                {
                    Button newButton = Instantiate(firstButton, new Vector3(firstButton.transform.position.x + (distance_x * z), firstButton.transform.position.y - distance_y, firstButton.transform.position.z), firstButton.transform.rotation, firstButton.transform.parent);
                    
                    buttonList.Add(newButton);
                    newButton.onClick.AddListener(()=>LevelButtonClick(newButton));
                }
            }
            distance_y = 250 * (y+1);
        }

        //distance_x = 0;
        //distance_y = 250;
        for (int x = 0; x < spriteList.Count; x++)
        {
            buttonList[x].GetComponent<Image>().sprite= spriteList[x];
            TagHelper.AddTag("Level" + (x+1));
            buttonList[x].gameObject.tag ="Level"+(x+1);
        }
        buttonList[0].transform.GetChild(0).gameObject.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void LevelButtonClick(Button btn)
    {
        if (btn.CompareTag("Level14"))
        {
            Debug.Log("tıkladı");
        }
        //switch (this.tag)
        //{
        //    case ("Level1"):
        //        Debug.Log("tıkladı");
        //        break;
        //}
    }
}
