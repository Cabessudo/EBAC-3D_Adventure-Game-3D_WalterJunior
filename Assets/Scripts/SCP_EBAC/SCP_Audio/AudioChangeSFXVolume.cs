using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChangeSFXVolume : AudioChangeVolumeBase
{
    // Start is called before the first frame update
    void Start()
    {
        LoadVolume(SaveManager.Instance.setup.sfx);
    }

    public override void SliderChageVolume(float currVolume)
    {
        base.SliderChageVolume(currVolume);
        SaveManager.Instance.SaveSFX((int)currVolume);
    }
}
