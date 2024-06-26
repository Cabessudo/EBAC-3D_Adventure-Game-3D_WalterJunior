using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Texts;

public class CheckPointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    private bool checkpointActivated;
    // private string checkpointSave = "checkpoint";
    public int checkpointKey = 01;

    void OnTriggerEnter(Collider other)
    {
        if(!checkpointActivated && other.gameObject.CompareTag("Player"))
        {
            checkpointActivated = true;
            CheckCheckpoint();
            SaveCheckpoint();
            TextManagerUI.Instance?.SetTextByType(TextType.CHECKPOINT);
        }
    }
  
    void CheckCheckpoint()
    {
        TurnItOn();
    }

    void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    [NaughtyAttributes.Button]
    void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.gray);
        checkpointActivated = false;
    }

    void SaveCheckpoint()
    {
        // if(PlayerPrefs.GetInt(checkpointSave, 0) > checkpointKey)
        //     PlayerPrefs.SetInt(checkpointSave, checkpointKey);

        CheckpointManager.Instance.SaveCheckpoint(checkpointKey);
        CheckpointManager.Instance.AddCheckpointToList(this);
    }
}
