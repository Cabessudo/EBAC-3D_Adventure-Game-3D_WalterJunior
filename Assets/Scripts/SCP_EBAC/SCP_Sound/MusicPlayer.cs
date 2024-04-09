using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public MusicType musicType;
    private MusicSetup _currSetup;
    public AudioSource musicAudio;

    // Start is called before the first frame update
    void Start()
    {
        Play();
    }

    void Play()
    {
        _currSetup = SoundManager.Instance.GetMusicByType(musicType);

        musicAudio.clip = _currSetup.music;
        musicAudio.Play();
    }
}
