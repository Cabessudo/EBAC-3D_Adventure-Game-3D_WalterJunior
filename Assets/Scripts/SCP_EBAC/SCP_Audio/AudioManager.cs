using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer group;
    public string sfxParam = "SFXParam";
    public string musicParam = "MusicParam";

    // Start is called before the first frame update
    void Start()
    {
        group?.SetFloat(sfxParam, SaveManager.Instance.setup.sfx);
        group?.SetFloat(musicParam, SaveManager.Instance.setup.music);
    }
}
