using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpoint;
    public List<CheckPointBase> checkPoints;

    void Start()
    {
        LoadCheckPoints();
    }

    void LoadCheckPoints()
    {
        var check = SaveManager.Instance.setup.checkPointPos;

        for(int i = 0; i < checkPoints.Count; i++)
        {
            if(lastCheckpoint < checkPoints[i].checkpointKey)
            {
                lastCheckpoint = checkPoints[i].checkpointKey;
            }
        }
    }

    public void SaveCheckpoint(int i)
    {
        if(i > lastCheckpoint)
            lastCheckpoint = i;
    }

    public void AddCheckpointToList(CheckPointBase currCheckpoint)
    {
        checkPoints.Add(currCheckpoint);
        SaveManager.Instance.SaveCheckpoints(currCheckpoint.transform.position);
    }

    public Vector3 GetLastCheckpointPos()
    {
        var lastCheckpointPos = checkPoints.Find(i => i.checkpointKey == lastCheckpoint);
        return lastCheckpointPos.transform.position;
    }

    public bool CheckpointsCheck()
    {
        return lastCheckpoint > 0;
    }
}
