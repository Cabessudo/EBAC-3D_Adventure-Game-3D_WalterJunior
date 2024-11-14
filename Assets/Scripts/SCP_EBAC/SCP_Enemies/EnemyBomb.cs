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
        //Variables
        public float timeToExplode = 2;
        public int explodeDamage = 5;
        
        //Animation
        public Ease easeExplosion = Ease.OutBack;

        //Explode
        public bool exploding;
        public float explodeSize = 1.5f;
        public float explodeDuration = 2;
        public bool playerOnRadius;

        public bool showExplodeRadius;
        public float explodeRadius;
        public float explosionForce = 2;

        // Update is called once per frame
        protected override void Update()
        {
            if(exploding) return;

            base.Update();            

            if(playerDetected && !attackState)
            {
                SwitchAttack();
            }
            
        }

        public override void EnemyUpdate()
        {
            CheckExplosionRadious();

            if(attackState && !playerOnRadius)
            {
                ChasePlayer();
            }

            if(playerOnRadius && attackState)
            {
                StartCoroutine(ExplodeRoutine());
            }

        }

        public IEnumerator ExplodeRoutine()
        {   
            anim.SetAnimByType(Anim.AnimEnemyType.ATTACK);
            yield return new WaitForSeconds(timeToExplode);
            ExplodeAnim();
        }

        #region Explode
        void ExplodeAnim()
        {
            //Explosion Animation
            exploding = true;
            anim.SetAnimByType(Anim.AnimEnemyType.IDLE);
            transform.DOScale(Vector3.one * explodeSize, explodeDuration).SetEase(easeExplosion);
            mesh.material.DOColor(Color.white, "_EmissionColor", explodeDuration).SetEase(easeExplosion).OnComplete(
                delegate{Explode();}
            );
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
            
            if(hurtPS != null) hurtPS.Play();
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
