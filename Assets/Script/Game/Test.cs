using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform waypoint;
    public float turnSpeed = 10;
    
    void Update()
    {
        LookDir();
    }

    void LookDir()
    {
        Vector3 lookDir = waypoint.position - new Vector3(transform.position.x, waypoint.position.y, transform.position.z);
        transform.forward = Vector3.Slerp(transform.forward, lookDir.normalized, Time.deltaTime * turnSpeed);
    }
}
