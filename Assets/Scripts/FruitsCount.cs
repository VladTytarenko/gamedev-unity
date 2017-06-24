using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsCount : MonoBehaviour {

    public static FruitsCount count;
    public int maxFruitQuantity = 0;
    public static int max = 0;
    UILabel label;

	void Start () {
        count = this;
        max = maxFruitQuantity;
        label = count.GetComponent<UILabel>();
        label.text = "0/" + maxFruitQuantity;
	}

    public void countFruitsLabel(int number)
    {
        label.text = number + "/" + maxFruitQuantity;
    }
	
   
}
