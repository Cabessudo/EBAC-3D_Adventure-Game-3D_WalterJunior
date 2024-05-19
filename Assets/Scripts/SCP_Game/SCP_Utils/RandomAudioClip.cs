using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioClip : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioClips;
    private int _index;

    public void PlayRandomAudioClip()
    {
        if(_index >= audioClips.Count) _index = 0;
        audioSource.clip = audioClips[_index];
        audioSource.Play();

        _index++;
    }
}
