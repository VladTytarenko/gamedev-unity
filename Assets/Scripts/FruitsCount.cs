using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsCount : MonoBehaviour {

    public static FruitsCount count;
    public int max = 0;
    UILabel label;

	void Start () {
        count = this;
        label = count.GetComponent<UILabel>();
        label.text = "0/" + max;
	}

    public void countFruitsLabel(int number)
    {
        label.text = number + "/" + max;
    }
	
}
