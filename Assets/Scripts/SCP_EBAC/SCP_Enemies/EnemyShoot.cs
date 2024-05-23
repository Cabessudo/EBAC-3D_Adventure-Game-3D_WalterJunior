using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        [Header("Shoot")]
        public GunBase enemyGun;
        private bool _attack;
        private bool _idle;
        protected bool canShoot = true;
        
        protected override void Update()
        {
            stateMachine.Update();
            CheckPlayer();
            
            if(!dead)
            {
                if(!playerDetected && !walkState)
                {
                    SwitchWalk();
                }

                if(playerDetected && !attackState)
                {
                    SwitchAttack();
                }
            }
        }

        public override void EnemyUpdate()
        {
            LookPlayer();
        }

        public override void Attack(Action action = null)
        {
            enemyGun.StartShoot(action);
        }

        public override void ActionAttack()
        {
            anim.SetAnimByType(Anim.AnimEnemyType.ATTACK);   
        }

        public override void Movement(Action action = null)
        {
            enemyGun.StopShoot();
            anim.SetAnimByType(Anim.AnimEnemyType.IDLE);
            base.Movement(action);
        }


        public override void OnDeath()
        {
            enemyGun?.StopAllCoroutines();
            enemyGun?.StopShoot();
            base.OnDeath();
        }
    }
}
