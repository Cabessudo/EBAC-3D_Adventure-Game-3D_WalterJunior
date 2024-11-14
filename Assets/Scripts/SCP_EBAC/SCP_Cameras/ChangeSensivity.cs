using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeSensivity : MonoBehaviour
{
    public CinemachineFreeLook cam;

    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
    }

    public void ChangeCamSensX(float currSensX)
    {
        cam.m_XAxis.m_MaxSpeed = currSensX;
        SaveManager.Instance.SaveCameraSensX(currSensX);   
    }

    public void ChangeCamSensY(float currSensY)
    {
        cam.m_YAxis.m_MaxSpeed = currSensY;
        SaveManager.Instance.SaveCameraSensY(currSensY);   
        Debug.Log("DAda");
    }
}
