using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCrystal : MonoBehaviour
{

    public static FirstCrystal crystal;

    void Start()
    {
        crystal = this;
        crystal.gameObject.GetComponent<UI2DSprite>().enabled = false;
    }

    public void findCrystal()
    {
        crystal.gameObject.GetComponent<UI2DSprite>().enabled = true;
    }

}