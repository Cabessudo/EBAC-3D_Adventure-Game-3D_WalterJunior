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
        

        // protected override void Update()
        // {
        //     if(!dead)
        //     {
        //         base.Update();
        //         if(playerDetected && _player.isAlive && canShoot)
        //         {
        //             _idle = false;
        //             LookPlayer();

        //             if(!_attack)
        //             {
        //                 enemyGun.StartShoot(Action);
        //                 _attack = true;
        //             }
        //         }
        //         else if(!playerDetected || !_player.isAlive || !canShoot)
        //         {
        //             _attack = false;
        //             enemyGun.StopShoot();

        //             if(!_idle)
        //             {
        //                 _idle = true;
        //                 anim.SetAnimByType(Anim.AnimEnemyType.IDLE);
        //             }
        //         }
        //     }
        //     else
        //         enemyGun.StopShoot();
        // }

        // void Action()
        // {
        //     anim.SetAnimByType(Anim.AnimEnemyType.ATTACK);   
        // }

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
            enemyGun.StartShoot(Action);
        }

        public override void Movement(Action action = null)
        {
            enemyGun.StopShoot();
            anim.SetAnimByType(Anim.AnimEnemyType.IDLE);
            base.Movement(action);
        }

        void Action()
        {
            anim.SetAnimByType(Anim.AnimEnemyType.ATTACK);   
        }

        public override void Death()
        {
            base.Death();
            enemyGun?.StopShoot();
        }
    }
}
