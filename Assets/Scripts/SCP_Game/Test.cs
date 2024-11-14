using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using Boss;
using Enemy;
using Cinemachine;

public class Test : MonoBehaviour
{
    public CinemachineFreeLook cam;
    public float sensSpeed;

    [NaughtyAttributes.Button]
    public void ChangeSen()
    {
        cam.m_XAxis.m_MaxSpeed = sensSpeed;
    }
}
