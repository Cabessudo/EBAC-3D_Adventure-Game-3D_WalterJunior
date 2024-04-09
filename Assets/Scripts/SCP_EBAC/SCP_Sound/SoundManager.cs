using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;

public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;
    public AudioSource mainAudio;

    // Start is called before the first frame update
    void Start()
    {
        Play(musicSetups[0].musicType);
    }

    void Play(MusicType musicType)
    {
        var audio = musicSetups.Find(i => i.musicType == musicType);
        mainAudio.clip = audio.music;
        mainAudio.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return musicSetups.Find(i => i.musicType == musicType);
    }

    public SFXSetup GetSFXByType(SFXType sfxType)
    {
        return sfxSetups.Find(i => i.sfxType == sfxType);
    }

}

public enum MusicType
{
    TYPE_01,
    TYPE_02,
    TYPE_03
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip music;
}

public enum SFXType
{
    TYPE_01,
    TYPE_02,
    TYPE_03
}

[System.Serializable]
public class SFXSetup
{
    public SFXType sfxType;
    public AudioClip sfx;
}
