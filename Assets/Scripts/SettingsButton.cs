using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour {

    public GameObject setting;
    public PauseButton button;

    void settingBoard()
    {
        GameObject obj = UICamera.first.transform.parent.gameObject;
        NGUITools.AddChild(obj, setting);
    }

	// Use this for initialization
	void Start () {
        button.signalOnClick.AddListener(settingBoard);  
	}
}
