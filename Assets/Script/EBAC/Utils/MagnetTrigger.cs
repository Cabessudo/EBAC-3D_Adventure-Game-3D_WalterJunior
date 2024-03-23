using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        ItemColletableBase item = other.gameObject.GetComponent<ItemColletableBase>();
        if(item != null && item.gameObject.AddComponent<Magnet>() == false)
        {
            item.gameObject.AddComponent<Magnet>();
            Debug.Log("fffaaaf");
        }
    }
}
