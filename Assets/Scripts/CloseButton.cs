using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour {

    public PauseButton closeB;

    void close()
    {
        SettingsBoard.board.destroy();
    }

	void Start () {
        closeB.signalOnClick.AddListener(close);
	}
}
