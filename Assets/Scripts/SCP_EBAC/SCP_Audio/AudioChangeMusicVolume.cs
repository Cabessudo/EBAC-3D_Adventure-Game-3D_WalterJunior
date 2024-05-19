using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChangeMusicVolume : AudioChangeVolumeBase
{
    // Start is called before the first frame update
    void Start()
    {
        LoadVolume(SaveManager.Instance.setup.music);
    }

    public override void SliderChageVolume(float currVolume)
    {
        base.SliderChageVolume(currVolume);
        SaveManager.Instance.SaveMusic((int)currVolume);
    }
}
