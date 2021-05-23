﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
   // public bool Hey { get; private set; }

    public GameObject uıPanel,gameOverPanel,settingsPanel,rainZone, oyuncak, rainDrop,door;
    public Button startButton;
    private bool hey=false,pause=false;
    public Animator startAnim,finishLevel;
    public ParticleSystem confetti;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private int poolNum;
    [SerializeField]
    private float doorWaitTime;
    public List<GameObject> rainDropList;
    public List<GameObject> emptyPotionList;
    public List<GameObject> potionListGameObject;
    public Sprite fullPotion;
    private int potionPickCount;

    private bool ok;

    // public AnimationCurve zCurve, yCurve;
    //[Range(0,100)] 
    //public float oyuncakSpeed;

    private void Awake()
    {
        gameOverPanel = uıPanel.transform.GetChild(0).gameObject;
        settingsPanel = uıPanel.transform.GetChild(1).gameObject;
    }
    void Start()
    {
        DoorClosed();
        UnHidePotionImage();
        potionPickCount = 0;
        rainDropList.Add(rainDrop);
        
        Time.timeScale = 1;
        if (Instance==null)
        {
            Instance = this;
        }
        uıPanel.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
        settingsPanel.SetActive(false);
        startButton.onClick.AddListener(StartAgainButton);

        for (int i = 0; i < poolNum; i++)
        {
            var currentRainDrop=Instantiate(rainDrop, new Vector3(Random.RandomRange(spawnPoint.transform.position.x - 2.5f, spawnPoint.transform.position.x + 2.5f) , spawnPoint.transform.position.y), rainDrop.transform.rotation);
            currentRainDrop.SetActive(false);
            currentRainDrop.transform.parent = rainDrop.transform.parent;
            rainDropList.Add(currentRainDrop);
        }

        for (int i = 0; i < rainDropList.Count; i++)
        {
            var currentRainDrop = rainDropList[i];
            currentRainDrop.GetComponent<Rigidbody2D>().gravityScale = Random.RandomRange(0.1f,2);
            currentRainDrop.SetActive(false);
            
        }

    }

    private void FixedUpdate()
    {
       // oyuncak.transform.eulerAngles += new Vector3(0, 0, -zCurve.Evaluate(1)) * Time.deltaTime * oyuncakSpeed;
       // oyuncak.transform.position+= new Vector3(0, -yCurve.Evaluate(1),0) * Time.deltaTime * oyuncakSpeed;
    }

    void Update()
    {
       // Debug.Log("ok "+ok);

    }

    public void GameOver()
    {
        startAnim.SetBool("isPlay", true);
        uıPanel.SetActive(true);
        gameOverPanel.SetActive(true);
        settingsPanel.SetActive(false);
        HidePotionImage();
    }

    public void FinishLevel()
    {
        DoorOpen();
        finishLevel.SetBool("isFinish", true);
        StartCoroutine(ClosedDoor(2.5f));
        UIManager.Instance.CurrentLevelSetter();
        // Debug.Log("cur"+ UIManager.Instance.CurrentLevel);
        HidePotionImage();
    }

    public void StartAgainButton()
    {
       // Debug.Log("clicked");
        SceneManager.LoadScene("Level_"+ UIManager.Instance.CurrentLevel);
    }

    public void SettingsButton()
    {
        ResetSettingsButton();
        HidePotionImage();
    }

    public void ResetSettingsButton()
    {
        uıPanel.SetActive(true);
        settingsPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        for (int i = 0; i < settingsPanel.transform.GetChild(0).GetChildCount(); i++)
        {
            settingsPanel.transform.GetChild(0).transform.GetChild(i).gameObject.SetActive(true);
        }
        settingsPanel.transform.GetChild(0).transform.GetChild(settingsPanel.transform.GetChild(0).GetChildCount()-1).gameObject.SetActive(false);
    }

    public void OkButton()
    {
        uıPanel.SetActive(false);
        UnHidePotionImage();
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene("Level_" + UIManager.Instance.CurrentLevel);
    }

    public void PauseButton()
    {
        pause = !pause;
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level_" + UIManager.Instance.CurrentLevel);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void HelpButton()
    {
        settingsPanel.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        settingsPanel.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        settingsPanel.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        settingsPanel.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true);
    }

    IEnumerator ClosedDoor(float waitTime)
    {
        yield return new WaitForSeconds(doorWaitTime);
        DoorClosed();
        movement.Destroy(movement.Instance.gameObject);
        uıPanel.gameObject.SetActive(true);
        uıPanel.transform.GetChild(2).gameObject.SetActive(true);
        confetti.gameObject.SetActive(true);
        confetti.Play();
    }

    public void Rain()
    {
        rainZone.SetActive(true);
        GameObject rain_ = GetRainDrop();
        if (rain_!=null)
        {
            rain_.transform.position = new Vector3(Random.RandomRange(spawnPoint.transform.position.x - 2.5f, spawnPoint.transform.position.x + 2.5f), spawnPoint.transform.position.y);
            rain_.SetActive(true);
        }
    }

    public GameObject GetRainDrop()
    {
        for (int i = 0; i < rainDropList.Count; i++)
        {
            var currentLevel = rainDropList[i];
            if (!currentLevel.activeInHierarchy)
            {
                return currentLevel;
            }
        }
        return null;
    }

    public void ExitRain()
    {
        rainZone.SetActive(false);
        foreach (var current in rainDropList)
        {
            current.SetActive(false);
        }
    }

    public void CheckPotionCount()
    {
        potionPickCount++;

        if (potionPickCount > 0)
        {
            emptyPotionList[potionPickCount - 1].GetComponent<Image>().sprite = fullPotion;
        }

        if (potionPickCount == emptyPotionList.Count)
        {
            DoorOpen();
        }
    }

    public void HidePotionImage()
    {
        foreach (var current in emptyPotionList)
        {
            current.GetComponent<Image>().enabled = false;
        }
    }

    public void UnHidePotionImage()
    {
        foreach (var current in emptyPotionList)
        {
            current.GetComponent<Image>().enabled = true;
        }
    }

    public void DoorOpen()
    {
        door.transform.GetChild(0).gameObject.SetActive(false);
        door.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void DoorClosed()
    {
        door.transform.GetChild(0).gameObject.SetActive(true);
        door.transform.GetChild(1).gameObject.SetActive(false);
    }
}
