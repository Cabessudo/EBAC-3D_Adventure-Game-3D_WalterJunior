using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        [Header("Shoot")]
        public GunBase enemyGun;
        public bool lookAtPlayer;

        protected override void Init()
        {
            base.Init();
            enemyGun.StartShoot();
        }

        protected override void Update()
        {
            if(lookAtPlayer)
            {
                LookPlayer();
            }
        }
    }
}
