using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class Jetpack : MonoBehaviour
{
    public Rigidbody playerRb;
    public AudioSource SFX_jetpack;
    public UIFillUpdater fuelUIFill;
    public List<ParticleSystem> VFX_jetpack;
    public float velocity = 5;
    public float duration = 1;
    public float _currDuration;
    public bool isOn;

    void Start()
    {
        Land();
    }

    void Update()
    {
        if(isOn)
        {
            JetpackActived();
        }
    }
    
    public void Land()
    {
        Stop();
        _currDuration = duration;
        if(fuelUIFill != null) fuelUIFill.UpdateValueEmpty(_currDuration, duration);
    }

    void JetpackActived()
    {
        if(_currDuration > 0)
        {
            playerRb.AddForce(Vector3.up * velocity, ForceMode.Force);
            _currDuration -= Time.deltaTime;
            if(fuelUIFill != null) fuelUIFill.UpdateValueEmpty(_currDuration, duration);
        }
        else if(_currDuration <= 0)
        {
            Stop();
        }
    }

    public void Active()
    {
        isOn = true;
        VFX_jetpack.ForEach(i => i.Play());
        if(SFX_jetpack != null) SFXManager.Instance.SetAudioByType(Audio.SFXType.PLAYER_JETPACK, SFX_jetpack);
    }

    public void Stop()
    { 
        isOn = false;
        VFX_jetpack.ForEach(i => i.Stop());
        if(SFX_jetpack != null) SFX_jetpack.Stop();
    }

    
}
