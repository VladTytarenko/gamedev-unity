using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
    public float waitTime = 3.5f;
    public float Speed = 2f;
    bool isMovingToA = false;
    float to_wait = 0;
   
    void Start()
    {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) <= 0.2f;
    }

    void Update()
    {
        to_wait -= Time.deltaTime;
        if (to_wait <= 0)
        {
            Vector3 target;
            if (isMovingToA)
            {
                target = this.pointA;
            }
            else
            {
                target = this.pointB;
            }
            Vector3 myPosition = this.transform.position;
            if (isArrived(target, myPosition))
            {
                isMovingToA = !isMovingToA;
                to_wait = this.waitTime;
            }
            else
            {
                Vector3 destionnation = target - myPosition;
                float move = this.Speed * Time.deltaTime;
                float distance = Vector3.Distance(destionnation, myPosition);
                Vector3 move_vector = destionnation.normalized * Mathf.Min(move, distance);
                this.transform.position += move_vector;

            }
        }

    }
}