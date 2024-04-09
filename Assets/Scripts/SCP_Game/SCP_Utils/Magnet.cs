using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float collectSpeed = 5;
    public float minDis = 1;

    void Update()
    {
        if(Vector3.Distance(transform.position, MyPlayer.Instance.transform.position) > minDis)
        {
            collectSpeed++;
            transform.position = Vector3.MoveTowards(transform.position, MyPlayer.Instance.transform.position, Time.deltaTime * collectSpeed);
        }
    }
}
