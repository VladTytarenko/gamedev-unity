using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : MonoBehaviour
{

    public AudioClip winMusic = null;

    public UI2DSprite[] crystals;

    public UILabel fruitsLabel;
    public UILabel coinsLabel;

    AudioSource winSource;

    void Start()
    {
        winSource = gameObject.AddComponent<AudioSource>();
        winSource.clip = winMusic;
        winSource.Play();
        setCrystals();
        setFruitsLabel();
        setCoinsLabel();
        saveStats();
    }

    void setCrystals()
    {
        for (int i = 0; i < crystals.Length; i++)
        {
            crystals[i].GetComponent<UI2DSprite>().enabled = LevelController.current.hasCrystal(i);
        }
    }

    void setFruitsLabel()
    {
        fruitsLabel.text = LevelController.current.getFruits().ToString() + '/' + FruitsCount.max.ToString();
    }

    void setCoinsLabel()
    {
        coinsLabel.text = "+" + LevelController.current.getCoins().ToString();
    }

    void saveStats()
    {
        HeroRabit.currentRabit.saveStats();
        LevelController.current.saveCoins();
    }

}
