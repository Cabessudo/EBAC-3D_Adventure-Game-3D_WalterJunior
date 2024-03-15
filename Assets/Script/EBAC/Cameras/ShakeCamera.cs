using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Ebac.Singleton;

public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineVirtualCamera cam;
    public float timeToShake;

    [Header("Camera Configs")]
    public float amplitudeShake = 3;
    public float frequencyShake = 3;
    public float timeShake = .1f;


    [NaughtyAttributes.Button]
    public void Shake()
    {
        Shake(amplitudeShake, frequencyShake, timeShake);
    }

    public void Shake(float amplitude, float frequency, float time = .1f)
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;

        timeToShake = time;
    }

    void ShakeStop()
    {
        timeToShake = 0;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToShake > 0)
            timeToShake -= Time.deltaTime;
        else
        {
            ShakeStop();
        }
    }
}
