using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life1 : MonoBehaviour {

    public static Life1 life = null;
	
	void Start () {
        life = this;
	}

    public void loseLife()
    {
        GameObject.Destroy(this.gameObject);
    }
}
