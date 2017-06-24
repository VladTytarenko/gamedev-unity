using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtom : MonoBehaviour {

    public PauseButton sound;
    bool isOn;

    public void onOff()
    {
        isOn = !isOn;
        gameObject.GetComponent<UI2DSprite>().enabled = isOn;
        SoundManager.manager.setSoundOn(isOn);
    }

	// Use this for initialization
	void Start () 
    {
        sound.signalOnClick.AddListener(onOff);
        isOn = SoundManager.manager.isSoundOn();
        gameObject.GetComponent<UI2DSprite>().enabled = isOn;	
	}
	
}
