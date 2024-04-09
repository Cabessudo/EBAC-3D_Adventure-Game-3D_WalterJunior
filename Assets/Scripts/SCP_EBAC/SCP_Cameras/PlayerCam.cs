using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Player")]
    public Transform player;
    public Transform orientation;

    [Header("Cam")]
    public Transform cam;

    void FixedUpdate()
    {
        if(MyPlayer.Instance.isAlive)
            PlayerOrientation();
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
}
