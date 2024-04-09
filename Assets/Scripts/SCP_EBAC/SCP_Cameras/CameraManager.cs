using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;

public class CameraManager : Singleton<CameraManager>
{
    public GameObject mainCam;

    public float delay = 1;
    public float time;

    [NaughtyAttributes.Button]
    public void FocusOnRocket()
    {
        StartCoroutine(FocusRoutine());
    }

    IEnumerator FocusRoutine()
    {
        yield return new WaitForSeconds(delay);
        mainCam.SetActive(false);
        yield return new WaitForSeconds(time);
        mainCam.SetActive(true);
    }
}
