using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anim;

public class GroundCheck : MonoBehaviour
{
    public bool grounded;
    public bool isFalling;
    private bool once;

    void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if(!once && MyPlayer.Instance.isAlive)
            {
                MyPlayer.Instance.anim?.SetAnimByType(AnimPlayerType.LAND);
                grounded = true;
                isFalling = true;
                once = true;
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            once = false;

            if(!MyPlayer.Instance.isJumping)
                isFalling = true;
        }
    }
}
