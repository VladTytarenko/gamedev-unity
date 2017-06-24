using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayButton : MonoBehaviour {

    public PauseButton button;

	void Start () {
        button.signalOnClick.AddListener(run);
	}
    
    void run () {
        SceneManager.LoadScene("LevelMenu");	
	}
}
