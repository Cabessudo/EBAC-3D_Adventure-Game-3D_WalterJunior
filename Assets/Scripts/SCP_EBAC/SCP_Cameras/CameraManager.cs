using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;

public class CameraManager : Singleton<CameraManager>
{
    [Header("Player CAM")]
    public PlayerCam playerCam;
    private KeyCode _aimInput = KeyCode.Mouse1;

    [Header("Rocket CAM")]
    public List<GameObject> mainCams;
    public float delay = 1;
    public float time;

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        if(Input.GetKeyDown(_aimInput))
        {
            playerCam.aim = !playerCam.aim;
            playerCam.SwitchCamera(playerCam.aim);
        }
    }

    #region Player Cams

    public void EnableAllCams()
    {
        mainCams.ForEach(i => i.SetActive(true));
    }

    public void DisableAllCams()
    {
        mainCams.ForEach(i => i.SetActive(false));
    }

    #endregion

    #region Rocket
    [NaughtyAttributes.Button]
    public void FocusOnRocket()
    {
        StartCoroutine(FocusRoutine());
    }

    IEnumerator FocusRoutine()
    {
        yield return new WaitForSeconds(delay);
        mainCams.ForEach(i => i.SetActive(false));
        yield return new WaitForSeconds(time);
        mainCams.ForEach(i => i.SetActive(true));
    }
    #endregion

    #region Cursor
    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FreeCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    #endregion
}
