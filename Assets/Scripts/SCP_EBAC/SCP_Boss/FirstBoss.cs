using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Boss
{
    public class FirstBoss : BossBase
    {
        [Header("Boss Attack")]
        public Transform bossShootPos;
        public GameObject bossShoot;
        public float shootHeight = 2;

        void Update()
        {
            if(!dead)
            {
                CheckPlayer();
                if(playerDetected && atkState)
                {
                    ShootLookDir();
                    LookPlayer();
                }
                else if(playerDetected && !atkState)
                    SwitchAttack();

                if(!playerDetected && !walkState)
                {
                    SwitchWalk();
                }
            }
        }

        #region  BossAtk
        public override IEnumerator AttackRoutine(Action onAction)
        {
            yield return new WaitForSeconds(timeToAtk);
            IdleAnim();

            int atk = 0;
            while(attackTimes > atk)
            {
                _bossAnim.SetAnimByType(Anim.AnimEnemyType.ATTACK);
                atk++;
                transform.DOScale(1.2f, .1f).SetLoops(2, LoopType.Yoyo);
                Shoot();
                yield return new WaitForSeconds(timeBtwAttack);
            }

            onAction?.Invoke();
        }


        void ShootLookDir()
        {
            Vector3 lookShootDir = _player.transform.position - new Vector3(transform.position.x, transform.position.y - shootHeight, transform.position.z);
            bossShootPos.forward = Vector3.Slerp(bossShootPos.forward, lookShootDir.normalized, Time.deltaTime * turnSpeed);
        }

        void Shoot()
        {
            ShootLookDir();

            Instantiate(bossShoot, bossShootPos.position, bossShootPos.rotation);
        }
        #endregion
    }
}
