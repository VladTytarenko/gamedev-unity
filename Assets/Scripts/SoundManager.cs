using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager manager = new SoundManager();

    bool soundIsOn = true;
    bool musicIsOn = true;

    SoundManager()
    {
        soundIsOn = PlayerPrefs.GetInt("sound", 1) == 1;
        musicIsOn = PlayerPrefs.GetInt("music", 1) == 1;
    }

    public bool isSoundOn()
    {
        return this.soundIsOn;
    }

    public void setSoundOn(bool isOn)
    {
        this.soundIsOn = isOn;
        int temp;
        if (soundIsOn)
            temp = 1;
        else
            temp = 0;
        PlayerPrefs.SetInt("sound", temp);
        PlayerPrefs.Save();
    }

    public bool isMusicOn()
    {
        return this.musicIsOn;
    }

    public void setMusicOn(bool isOn)
    {
        this.musicIsOn = isOn;
        int temp;
        if (musicIsOn)
            temp = 1;
        else
            temp = 0;
        PlayerPrefs.SetInt("music", temp);
        PlayerPrefs.Save();
    }

    

}