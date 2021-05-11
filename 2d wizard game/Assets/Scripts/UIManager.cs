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
        if (Instance != null) {
            Destroy (UIManager.Instance);
        } else {
            Instance = this;
            DontDestroyOnLoad (UIManager.Instance);
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

      //  Debug.Log("awake");

        if (PlayerPrefs.GetInt("pref")==0)
        {
            for (int i = 0; i < UIManager.Instance.currentLevel; i++)
            {
                buttonList[i].transform.GetChild(0).gameObject.SetActive(false);
            }

            for (int i = 0; i < (UIManager.Instance.currentLevel - 1); i++)
            {
                buttonList[i].interactable = false;
            }
        }
        else
        {
            for (int i = 0; i < PlayerPrefs.GetInt("pref"); i++)
            {
                buttonList[i].transform.GetChild(0).gameObject.SetActive(false);
            }

            for (int i = 0; i < (PlayerPrefs.GetInt("pref") - 1); i++)
            {
                buttonList[i].interactable = false;
            }
        }
    }

    void Update()
    {
        if (PlayerPrefs.HasKey("pref"))
        {
            PlayerPrefs.SetInt("pref", UIManager.Instance.currentLevel);
            PlayerPrefs.Save();
        }
        
          //  Debug.Log("yok");

       // Debug.Log("prefupdate"+ PlayerPrefs.GetInt("pref"));
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
                currentLevel = i + 1;
            }
        }
    }

    public void CurrentLevelSetter()
    {
        UIManager.Instance.CurrentLevel += 1;
    }

    private void OnApplicationQuit()
    {
       
    }

}
