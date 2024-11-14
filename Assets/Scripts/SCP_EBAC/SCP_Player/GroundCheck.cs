using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anim;

public class GroundCheck : MonoBehaviour
{
    private Coroutine _currRoutine;
    public bool grounded;
    public bool isFalling;
    public bool onceLand;
    public float time = .1f;

    void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if(_currRoutine != null)
            {
                StopAllCoroutines();
                grounded = true; //When the game start this can't be triggered because there's no coroutine
            }

            if(!onceLand && MyPlayer.Instance.isAlive)
            {
                if(isFalling)
                    MyPlayer.Instance.anim?.SetAnimByType(AnimPlayerType.LAND);

                grounded = true;
                isFalling = false;
                onceLand = true;
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            _currRoutine = StartCoroutine(ExitGroundRoutine());

            if(!MyPlayer.Instance.isJumping && MyPlayer.Instance.isAlive)
                StartCoroutine(IsFallingRoutine());
        }
    }

    IEnumerator IsFallingRoutine()
    {
        yield return new WaitForSeconds(time);
        isFalling = true;
    }

    IEnumerator ExitGroundRoutine()
    {
        yield return new WaitForSeconds(time);
        onceLand = false;
    }
}
