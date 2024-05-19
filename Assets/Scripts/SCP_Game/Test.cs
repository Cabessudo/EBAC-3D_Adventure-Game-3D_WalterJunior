using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using Boss;
using Enemy;

public class Test : MonoBehaviour
{
    public MyPlayer player;
    public EnemyBase enemy;
    public List<Transform> waypoint;
    public int _index;
    public float turnSpeed = 50;
    public bool move;

    void Start()
    {
        Movement();
    }

    void Update()
    {
        LookDir();

    }

    // void Movement()
    // {
    //     if(!move) index++;
    //     if(index >= waypoint.Count) index = 0;
    //     move = true;

    //     var pos = new Vector3(waypoint[index].position.x, 0, waypoint[index].position.z);

    //     if(Vector3.Distance(transform.position, waypoint[index].position) > .1f)
    //     {
    //         transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 10);
    //     }
        
    //     if(Vector3.Distance(transform.position, pos) <= .1f)
    //     {
    //         move = false;
    //     }
    // }

    void Movement(Action action = null)
    {
        ChangeWayPoint();
        StartCoroutine(MoveRoutine(action));
    }

    void ChangeWayPoint()
    {
        _index++;
        if(_index >= waypoint.Count) _index = 0;
    }

    void Action()
    {
        move = true;
    }

    IEnumerator MoveRoutine(Action action)
    { 
        var pos = new Vector3(waypoint[_index].position.x, 0, waypoint[_index].position.z);

        while(Vector3.Distance(transform.position, waypoint[_index].position) > .1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 10);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);
        action?.Invoke();
    }

    public void LookDir()
    {
        // if(waypoint.Count > 0)
        // {
            var lookDir = waypoint[_index].position - new Vector3(transform.position.x, waypoint[_index].position.y, transform.position.z);
            transform.forward = Vector3.Slerp(transform.forward, lookDir.normalized, Time.deltaTime * turnSpeed);
        // }
    }
}
