using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    public class EnemyBomb : EnemyBase
    {
        [Header("Bomb")]
        private Coroutine _currRoutine;
        //Variables
        public bool _explode;
        public float timeToExplode = 2;
        public int explodeDamage = 5;
        public float gingle = 50;
        
        //Animation
        public Ease ease = Ease.OutBack;
        public float zAngleFloat = 10f;
        private Quaternion zAngle;

        //Explode
        public MeshRenderer mesh;
        public float explodeSize = 1.5f;
        public float explodeDuration = 2;
        public bool playerOnRadius;

        public bool showExplodeRadius;
        public float explodeRadius;
        public float explosionForce = 2;

        public bool once;


        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            CheckPlayer();
            CheckExplosionRadious();

            if(playerDetected && !playerOnRadius && !_explode)
            {
                LookPlayer();
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }

            if(playerOnRadius)
            {
                _currRoutine = StartCoroutine(ActiveRoutine());
            }
            
        }

        #region Active
        IEnumerator ActiveRoutine()
        {
            if(!_explode)
            {
                StartCoroutine(ActiveBombRoutine());
                yield return new WaitForSeconds(timeToExplode);
                _explode = true;
            }
            else if(_explode && !once)
            {
                StartCoroutine(ExplodeRoutine());
                once = true;
            }
            
        }

        IEnumerator ActiveBombRoutine()
        {
            while(true)
            {
                Active();
                yield return new WaitForSeconds(.1f);
                Active(-1);
                yield return new WaitForSeconds(.1f);
            }
        }

        void Active(int side = 1)
        {  
            zAngle =  Quaternion.Euler(transform.rotation.x, transform.rotation.y, zAngleFloat * side);
            transform.rotation = Quaternion.Slerp(transform.rotation, zAngle, Time.deltaTime * gingle);
        }

        #endregion

        #region Explode

        IEnumerator ExplodeRoutine()
        {
            ExplodeAnim();
            yield return new WaitForSeconds(explodeDuration);
            Explode();
            
        }

        void ExplodeAnim()
        {
            //Explosion Animation
            var angle = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, angle, Time.deltaTime * gingle);
            transform.DOScale(Vector3.one * explodeSize, explodeDuration).SetEase(ease);
            mesh.material.DOColor(Color.white, "_EmissionColor", explodeDuration).SetEase(ease);
        }

        void Explode()
        {
            mesh.enabled = false;
            foreach(var sprites in transform.GetComponentsInChildren<SpriteRenderer>())
                sprites.enabled = false;

            //Damage On Player
            if(playerOnRadius)
            {
                ShakeCamera.Instance.Shake();
                var dir = _player.transform.position - transform.position;
                _player.playerHealth.Damage(explodeDamage, dir.normalized, explosionForce);
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
