using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioChangeVolumeBase : MonoBehaviour
{
    public AudioMixer group;
    public TMPro.TextMeshProUGUI volumeText;
    public Slider slider;
    public string floatParam;

    public void LoadVolume(float currVolume)
    {
        //Put the Slider on the rigth volume
        slider.value = currVolume;
    }

    public virtual void SliderChageVolume(float currVolume)
    {
        group?.SetFloat(floatParam, currVolume);
    }
}
