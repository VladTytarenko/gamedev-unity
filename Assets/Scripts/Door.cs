using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour
{

    public string level = "LevelMenu";
    public string openLevel = "isOpen";
    public GameObject lockObj, crystObj, fruitObj;
    LevelInfo stats;
    bool isOpen = false;

    void Start()
    {
        string str = PlayerPrefs.GetString(level);
        stats = JsonUtility.FromJson<LevelInfo>(str);
        if (stats == null)
        {
            stats = new LevelInfo();
        }
       
        string str2 = PlayerPrefs.GetString(openLevel);
        LevelInfo openLevelStats = JsonUtility.FromJson<LevelInfo>(str2);
        if (openLevelStats == null)
        {
            openLevelStats = new LevelInfo();
        }
        isOpen = openLevelStats.levelFinish;

        if (openLevel == "isOpen")
            isOpen = true;

        if (stats.levelFinish) 
            Destroy(lockObj);
        if (!stats.crystals) 
            Destroy(crystObj);
        if (!stats.fruits) 
            Destroy(fruitObj);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        HeroRabit rabit = collider.GetComponent<HeroRabit>();
        if (rabit != null && isOpen)
            SceneManager.LoadScene(level);
    }

}