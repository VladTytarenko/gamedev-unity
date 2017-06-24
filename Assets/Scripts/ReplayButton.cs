using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ReplayButton : MonoBehaviour
{

    public PauseButton reloadButton;

    void replayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        reloadButton.signalOnClick.AddListener(replayLevel);
    }

}