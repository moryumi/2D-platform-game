using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
   // public bool Hey { get; private set; }

    public GameObject uıPanel,gameOverPanel,settingsPanel;
    public Button startButton;
    //public GameObject character;
    public GameObject oyuncak;
    private bool hey=false,pause=false;
    public Animator startAnim, oyuncakSalınım,finishLevel;
    //public GameObject Door;

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
        
        Time.timeScale = 1;
        if (Instance==null)
        {
            Instance = this;
        }
        //startAnim.updateMode = AnimatorUpdateMode.UnscaledTime;

        uıPanel.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
        settingsPanel.SetActive(false);
        startButton.onClick.AddListener(StartAgainButton);
        oyuncakSalınım.SetBool("isOyuncakSalınım", true);
    }

    private void FixedUpdate()
    {
       // oyuncak.transform.eulerAngles += new Vector3(0, 0, -zCurve.Evaluate(1)) * Time.deltaTime * oyuncakSpeed;
       // oyuncak.transform.position+= new Vector3(0, -yCurve.Evaluate(1),0) * Time.deltaTime * oyuncakSpeed;
    }

    void Update()
    {
       // Debug.Log(finishLevel.GetCurrentAnimatorStateInfo(0).length);
    }

    public void GameOver()
    {
        startAnim.SetBool("isPlay", true);
        uıPanel.SetActive(true);
        gameOverPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void FinishLevel(GameObject obj)
    {
        finishLevel.SetBool("isFinish", true);
        StartCoroutine(ClosedDoor(5, obj));
    }

    public void StartAgainButton()
    {
       // Debug.Log("clicked");
        SceneManager.LoadScene("2d_wizard_game");
    }

    public void SettingsButton()
    {
        ResetSettingsButton();
       
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
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene("2d_wizard_game");
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

    public void HelpButton()
    {
        settingsPanel.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        settingsPanel.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        settingsPanel.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        settingsPanel.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true);
    }

    IEnumerator ClosedDoor(float waitTime,GameObject obj)
    {
        yield return new WaitForSeconds(waitTime);
        obj.transform.GetChild(0).gameObject.SetActive(true);
        obj.transform.GetChild(1).gameObject.SetActive(false);
    }

}
