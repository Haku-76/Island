using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHorztal : MonoBehaviour
{
    private Transform target;

    private Vector3 offest;

    private Vector3 StartPostion;

    Vector3 travel => target.position - StartPostion;

    public float speed;
    void Start()
    {
        offest = target.position - transform.position;
        StartPostion = transform.position;
    }

    
    void Update()
    {
        transform.position = StartPostion + new Vector3(travel.x * speed, 0, 0);
    }
}
