using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life3 : MonoBehaviour {

    public static Life3 life = null;

    void Start()
    {
        life = this;
    }

    public void loseLife()
    {
        GameObject.Destroy(this.gameObject);
    }
}
