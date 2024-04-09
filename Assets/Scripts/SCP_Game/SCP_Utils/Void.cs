using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Fall From The World");
            var player = other.gameObject.GetComponent<MyPlayer>();

            if(player != null)
                player.Revive();
        }
    }
}
