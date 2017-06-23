using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsQuantity : MonoBehaviour
{

    public static CoinsQuantity coins;

    UILabel label;

    void Start()
    {
        coins = this;
        label = coins.GetComponent<UILabel>();
        label.text = "000";
    }

    public void labelText(int num)
    {
        string newLabel = "";
        int length = num.ToString().Length;

        if (length == 1)
            newLabel = "00" + (num - 1);
        else if (length == 2)
            newLabel = "0" + (num - 1);
        else
            newLabel = "" + (num - 1);
        label.text = newLabel;
    }
}

