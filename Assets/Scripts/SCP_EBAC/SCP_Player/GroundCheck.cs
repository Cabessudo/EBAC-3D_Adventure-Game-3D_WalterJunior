using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anim;

public class GroundCheck : MonoBehaviour
{
    public bool grounded;
    public bool isFalling;

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            MyPlayer.Instance.anim?.SetAnimByType(AnimType.LAND);
            grounded = true;
            isFalling = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;

            if(!MyPlayer.Instance.isJumping)
                isFalling = true;
        }
    }
}
