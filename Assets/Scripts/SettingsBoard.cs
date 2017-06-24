using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBoard : MonoBehaviour {

    public static SettingsBoard board;

	// Use this for initialization
	void Start () {
        board = this;
	}
	
	public void destroy() 
    {
        Destroy(board.gameObject);
	}
}
