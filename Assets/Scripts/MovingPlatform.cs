using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public float spacing = 1.92f;
    public float num = 3f;
    float direction = 1;
    float minX, maxX;

    void Start () {
        minX = transform.position.x;
        maxX = minX + num * spacing;
    }

    void Update () {
        Vector3 currPos = transform.position;
        if (currPos.x >= maxX) {
            direction = -1;
        }
        if (currPos.x <= minX) {
            direction = 1;
        }
        //time_to_wait -= Time.deltaTime;
        //if (time_to_wait <= 0)
        transform.Translate(new Vector3(direction * 5f * Time.deltaTime, 0, 0));
    }

}
