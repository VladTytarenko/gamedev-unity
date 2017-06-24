using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{

    public PauseButton musicButton;
    bool isOn = true;

    public void onOff()
    {
        isOn = !isOn;
        gameObject.GetComponent<UI2DSprite>().enabled = isOn;
        SoundManager.manager.setMusicOn(isOn);
        if (isOn) 
            Music.foneMusic.turnOn();
        else 
            Music.foneMusic.turnOff();
    }

    void Start()
    {
        musicButton.signalOnClick.AddListener(onOff);
        isOn = SoundManager.manager.isMusicOn();
        gameObject.GetComponent<UI2DSprite>().enabled = isOn;
    }

   

}