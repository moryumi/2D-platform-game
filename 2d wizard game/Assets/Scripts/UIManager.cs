using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public List<Sprite> spriteList;
    public Button sampleButton;
    public List<Button> buttonList;
    [SerializeField]
    private int distance_x, distance_y=0;
    private Button firstButton;
    private int currentLevel;
    public int CurrentLevel { get { return currentLevel; }  set{ currentLevel = value; } }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        for (int i = 0; i < 15; i++)
        {
            TagHelper.AddTag("Level" + (i + 1));            //SpriteList loopunda çalışmadı?
        }
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

        for (int x = 0; x < spriteList.Count; x++)
        {
            buttonList[x].gameObject.name = "Button_Level_" + (x+1);
            buttonList[x].GetComponent<Image>().sprite= spriteList[x];
            buttonList[x].gameObject.tag ="Level"+(x+1);
        }
        buttonList[0].transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
       
    }

    public void LevelButtonClick(Button btn)
    {
        for (int i = 0; i < 15; i++)
        {
            if (btn.CompareTag("Level" + (i+1)))
            {
                //Debug.Log("tıkladı "+(i + 1));
                SceneManager.LoadScene("Level_"+(i+1));
                currentLevel = i + 1;
            }
        }
    }
}
