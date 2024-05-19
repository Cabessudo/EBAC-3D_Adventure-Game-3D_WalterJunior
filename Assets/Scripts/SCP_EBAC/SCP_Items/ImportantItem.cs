using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantItem : MonoBehaviour
{
    public bool canCollect;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && canCollect)
        {
            Collect();
            MyPlayer.Instance.ActiveJetpackCheck();
            SaveManager.Instance.SaveJetpack();
            Destroy(gameObject, .1f);
        }
    }

    void Collect()
    {
        LevelManager.Instance.NextLevel();
        CameraManager.Instance.FocusOnRocket();
    }
}
