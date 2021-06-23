using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerpref : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("playerpref start");
    }
    void Start()
    {
        //UIManager.Instance.CurrentLevel = PlayerPrefs.GetInt("pref");
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("pref + "+ PlayerPrefs.GetInt("pref"));
        if (PlayerPrefs.HasKey("pref"))
        {
            //Debug.Log("pref var");
            PlayerPrefs.SetInt("pref", UIManager.Instance.CurrentLevel);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("pref yok");
        }
        
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("pref", UIManager.Instance.CurrentLevel);
        PlayerPrefs.Save();
    }
}
