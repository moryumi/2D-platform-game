using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject uıPanel, gameOverPanel, settingsPanel;
    public static UIManager Instance { get; private set; }
    public List<Sprite> spriteList;
    public Button sampleButton;
    public List<Button> buttonList;
    private int distance_y = 0;
    [SerializeField]
    private int distance_x, distance_y2;
    private Button firstButton;
    public int currentLevel;
    public int CurrentLevel { get { return currentLevel; }  set{ currentLevel = value; } }
    private bool hey = false, pause = false;

    void Awake()
    {
        currentLevel= PlayerPrefs.GetInt("pref");
        if (Instance != null) {
           // Destroy (UIManager.Instance);
        } else {
            Instance = this;
            DontDestroyOnLoad (UIManager.Instance);
        }

        //gameOverPanel = uıPanel.transform.GetChild(0).gameObject;
        //settingsPanel = uıPanel.transform.GetChild(1).gameObject;
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
                    newButton.interactable = false;
                    newButton.onClick.AddListener(()=>LevelButtonClick(newButton));
                }
            }
            distance_y = distance_y2 * (y+1);
        }

        for (int x = 0; x < spriteList.Count; x++)
        {
            buttonList[x].gameObject.name = "Button_Level_" + (x+1);
            buttonList[x].GetComponent<Image>().sprite= spriteList[x];
            buttonList[x].gameObject.tag ="Level"+(x+1);
        }

        buttonList[0].transform.GetChild(0).gameObject.SetActive(false);
        buttonList[0].interactable = true;

        

        //for (int i = 0; i < (UIManager.Instance.currentLevel - 1); i++)
        //{
        //    buttonList[i].interactable = false;
        //}
    }

    private void Start()
    {
        for (int i = 0; i < UIManager.Instance.currentLevel; i++)
        {
            buttonList[i].transform.GetChild(0).gameObject.SetActive(false);
            buttonList[i].interactable = false;
            if (i == UIManager.Instance.currentLevel - 1)
            {
                buttonList[i].interactable = true;
            }
        }
        Debug.Log("UI mAnager start");
        uıPanel.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    void Update()
    {
       
        // Debug.Log("pref is " + PlayerPrefs.GetInt("pref"));
    }

    public void LevelButtonClick(Button btn)
    {
        for (int i = 0; i < 15; i++)
        {
            if (btn.CompareTag("Level" + (i+1)))
            {
                //Debug.Log("tıkladı "+(i + 1));
                SceneManager.LoadScene("Level_"+(i+1));
                gameObject.SetActive(false);
                UIManager.Instance.currentLevel = i + 1;
            }
        }
    }



    public void CurrentLevelSetter()
    {
        UIManager.Instance.currentLevel += 1;
    }


}
