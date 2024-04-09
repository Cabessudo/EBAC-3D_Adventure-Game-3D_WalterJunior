using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartCheck : MonoBehaviour
{
    public GameObject bossCam;
    public Color radiusColor;


    void Awake()
    {
        bossCam.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            bossCam.SetActive(true);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = radiusColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
    }
}
