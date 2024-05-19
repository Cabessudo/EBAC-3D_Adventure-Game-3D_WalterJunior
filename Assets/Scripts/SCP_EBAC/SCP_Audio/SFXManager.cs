using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;

namespace Audio
{
    public enum SFXType
    {
        PLAYER_JUMP,
        PLAYER_HURT,
        PLAYER_GUN,
        PLAYER_JETPACK,
        PLAYER_FLAMETHROWER,
        ITEM_COIN,
        ENEMY_HURT,
        ENEMY_DEATH,
        ENEMY_EXPLOSION,
        BOSS_LAND
    }

    public class SFXManager : Singleton<SFXManager>
    {
        public List<AudioSetup> audioSetup;

        public void SetAudioByType(SFXType currType, AudioSource currAudio = null)
        {
            var setup = audioSetup.Find(i => i.type == currType);
            // if(currAudio.clip != null) currAudio.Stop();
            currAudio.clip = setup.sfx;
            currAudio.Play();
        }

        [System.Serializable]
        public class AudioSetup
        {
            public SFXType type;
            public AudioClip sfx;
        }
    }
}
