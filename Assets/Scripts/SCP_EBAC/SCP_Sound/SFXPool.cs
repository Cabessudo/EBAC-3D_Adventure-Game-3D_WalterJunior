using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;

public class SFXPool : Singleton<SFXPool>
{
    public int amount = 25;
    private int _index;
    public List<AudioSource> _audioSourceList;

    // Start is called before the first frame update
    void Start()
    {
        CreatePools();
    }

    void CreatePools()
    {
        _audioSourceList = new List<AudioSource>();
        for(int i = 0; i < amount; i++)
        {
            var go = new GameObject("SFX Pool");
            go.transform.SetParent(gameObject.transform);
            _audioSourceList.Add(go.AddComponent<AudioSource>());
        }
    }

    public void Play(SFXType sfxType)
    {
        var sfxSound = SoundManager.Instance.GetSFXByType(sfxType);
        _audioSourceList[_index].clip = sfxSound.sfx;
        _audioSourceList[_index].Play();

        _index++;
        if(_index > _audioSourceList.Count)
            _index = 0;
    }
}
