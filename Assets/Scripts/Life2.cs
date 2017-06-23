using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life2 : MonoBehaviour {

    public static Life2 life = null;

    void Start()
    {
        life = this;
    }

    public void loseLife()
    {
        GameObject.Destroy(this.gameObject);
    }
}
