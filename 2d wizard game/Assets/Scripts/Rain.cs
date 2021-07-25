using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rain : MonoBehaviour
{
    public static Rain Instance { get; private set; }
    // public bool Hey { get; private set; }

    public GameObject rainDrop, rainZone;
    
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private int poolNum;
    public List<GameObject> rainDropList;
    public List<GameObject> readyRainDropList;

    //void Start()
    //{
    //    //if (Instance == null)
    //    //{
    //    //    Instance = this;
    //    //}

    //    //rainDropList.Add(rainDrop);
        
    //    //for (int i = 0; i < poolNum; i++)
    //    //{
    //    //    var currentRainDrop = Instantiate(rainDrop, new Vector3(Random.RandomRange(spawnPoint.transform.position.x - 2.5f, spawnPoint.transform.position.x + 2.5f), spawnPoint.transform.position.y), rainDrop.transform.rotation);
    //    //    currentRainDrop.SetActive(false);
    //    //    currentRainDrop.transform.parent = rainDrop.transform.parent;
    //    //    rainDropList.Add(currentRainDrop);
    //    //}

    //    //for (int i = 0; i < rainDropList.Count; i++)
    //    //{
    //    //    var currentRainDrop = rainDropList[i];
    //    //    currentRainDrop.GetComponent<Rigidbody2D>().gravityScale = Random.RandomRange(0.1f, 1.2f);
    //    //    currentRainDrop.SetActive(false);

    //    //}

    //}

    //private void Update()
    //{
    //    readyDrops();
    //}

    //public void RainStart()
    //{
    //    rainZone.SetActive(true);
    //    GameObject rain_ = GetRainDrop();
    //    if (rain_ != null)
    //    {
    //        rain_.transform.position = new Vector3(Random.RandomRange(spawnPoint.transform.position.x - 2.5f, spawnPoint.transform.position.x + 2.5f), spawnPoint.transform.position.y);
    //        rain_.SetActive(true);
    //    }
    //}

    //public GameObject GetRainDrop()
    //{
    //    for (int i = 0; i < rainDropList.Count; i++)
    //    {
    //        var currentLevel = rainDropList[i];
    //        if (!currentLevel.activeInHierarchy)
    //        {
    //            return currentLevel;
    //        }
    //    }
    //    return null;
    //}

    //public void ExitRain()
    //{
    //    rainZone.SetActive(false);
    //    foreach (var current in rainDropList)
    //    {
    //        current.SetActive(false);
    //        readyRainDropList.Add(current);
    //    }
    //}

    //public void readyDrops()
    //{
    //    if (readyRainDropList.Count!=4)
    //    {
    //    }
    //}

    //IEnumerator waitDrops()
    //{
    //    yield return new WaitForSeconds(2f);
    //}

}
