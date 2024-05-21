using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCam : MonoBehaviour
{
    [Header("Player")]
    public Transform player;
    public Transform orientation;
    public Transform combatPos;

    [Header("Cam")]
    public CinemachineFreeLook defaultCam;
    public CinemachineFreeLook aimCam;
    public GameObject aimImage;
    public bool aim;

    void FixedUpdate()
    {
        if(!aim)
            PlayerOrientation();

        if(aim)    
            AimOrientation();  
    }

    void PlayerOrientation()
    {
        Vector3 lookDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = lookDir.normalized;
        
        float vt = Input.GetAxis("Vertical");
        float ht = Input.GetAxis("Horizontal");

        Vector3 movementDir = orientation.forward * vt + orientation.right * ht;

        if(movementDir != Vector3.zero)
        {
            player.forward = Vector3.Slerp(player.forward, movementDir.normalized, Time.deltaTime * 16);
        }
    }

    void AimOrientation()
    {
        Vector3 lookDir = combatPos.position - new Vector3(transform.position.x, combatPos.position.y, transform.position.z);
        orientation.forward = lookDir.normalized;
        player.forward = lookDir.normalized;
    }

    public void SwitchCamera(bool b)
    {
        DisableAllCams();

        if(!b)
        {
            defaultCam.enabled = true;
            aimImage.SetActive(b);
        }
        else
        {
            aimCam.enabled = true;
            aimImage.SetActive(b);
        }
    }

    public void DisableAllCams()
    {
        defaultCam.enabled = false;
        aimCam.enabled = false;
    }

    public void EnabledAllCams()
    {
        defaultCam.enabled = true;
        aimCam.enabled = true;
    }
}
