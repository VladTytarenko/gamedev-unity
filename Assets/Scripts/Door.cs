using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour
{

    public string level = "LevelMenu";

    public string openLevel = "isOpenAlsway";

    public GameObject lockObj;
    public GameObject crystObj;
    public GameObject fruitObj;

    LevelInfo stats;

    bool isOpen = false;

    void Start()
    {
        setStats();
        setIsLocked();
        showAtributes();
    }

    void setStats()
    {
        string str = PlayerPrefs.GetString(level);
        stats = JsonUtility.FromJson<LevelInfo>(str);
        if (stats == null)
        {
            stats = new LevelInfo();
        }
    }

    void setIsLocked()
    {
        string str = PlayerPrefs.GetString(openLevel);
        LevelInfo openLevelStats = JsonUtility.FromJson<LevelInfo>(str);
        if (openLevelStats == null)
        {
            openLevelStats = new LevelInfo();
        }
        isOpen = openLevelStats.levelFinish;

        if (openLevel == "isOpenAlsway")
            isOpen = true;
    }

    void showAtributes()
    {
        if (stats.levelFinish) Destroy(lockObj);
        if (!stats.crystals) Destroy(crystObj);
        if (!stats.fruits) Destroy(fruitObj);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        HeroRabit rabit = collider.GetComponent<HeroRabit>();
        if (rabit != null && isOpen)
            SceneManager.LoadScene(level);
    }

}