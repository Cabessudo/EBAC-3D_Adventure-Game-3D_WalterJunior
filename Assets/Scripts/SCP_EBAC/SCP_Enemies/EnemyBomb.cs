using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace Enemy
{
    public class EnemyBomb : EnemyBase
    {
        [Header("Bomb")]
        private Coroutine _currRoutine;
        //Variables
        public float timeToExplode = 2;
        public int explodeDamage = 5;
        public float gingle = 50;
        
        //Animation
        public Ease easeExplosion = Ease.OutBack;
        public float zAngleFloat = 10f;
        private Quaternion zAngle;

        //Explode
        public float explodeSize = 1.5f;
        public float explodeDuration = 2;
        public bool playerOnRadius;

        public bool showExplodeRadius;
        public float explodeRadius;
        public float explosionForce = 2;


        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            CheckExplosionRadious();
            base.Update();            

            if(playerOnRadius && !attackState)
            {
                SwitchAttack();
            }
            
        }

        public override void EnemyUpdate()
        {
            if(playerDetected && !playerOnRadius)
            {
                ChasePlayer();
            }
        }

        public override IEnumerator AttackRoutine(Action action)
        {   
            anim.SetAnimByType(Anim.AnimEnemyType.ATTACK);
            yield return new WaitForSeconds(timeToExplode);
            ExplodeAnim();
            yield return new WaitForSeconds(explodeDuration);
            Explode();
        }

        #region Active
        IEnumerator ActiveRoutine()
        {
            anim.SetAnimByType(Anim.AnimEnemyType.ATTACK);
            yield return new WaitForSeconds(timeToExplode);
            ExplodeAnim();
            yield return new WaitForSeconds(explodeDuration);
            Explode();
        }


        #endregion

        #region Explode
        void ExplodeAnim()
        {
            //Explosion Animation
            anim.SetAnimByType(Anim.AnimEnemyType.IDLE);
            var angle = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, angle, Time.deltaTime * gingle);
            transform.DOScale(Vector3.one * explodeSize, explodeDuration).SetEase(easeExplosion);
            mesh.material.DOColor(Color.white, "_EmissionColor", explodeDuration).SetEase(easeExplosion);
        }

        public void Explode()
        {
            Death();

            //Damage On Player
            if(playerOnRadius)
            {
                ShakeCamera.Instance.Shake();
                var dir = _player.transform.position - transform.position;
                _player.health.Damage(explodeDamage, dir.normalized, explosionForce);
            }
            
            if(hurtPS != null) hurtPS.Emit(100);
            Destroy(gameObject, 1);
        }


        void CheckExplosionRadious()
        {
            var onExplosionRange = Physics.OverlapSphere(transform.position, explodeRadius, playerMask);

            if(onExplosionRange.Length > 0)
            {
                playerOnRadius = true;
                explodeRadius = 10;
            }
            else
                playerOnRadius = false;
        }

        protected override void OnDrawGizmosSelected()
        {
            if(!showExplodeRadius)
               base.OnDrawGizmosSelected();
            else
                Gizmos.DrawWireSphere(transform.position, explodeRadius);
        }
        #endregion
    }
}
