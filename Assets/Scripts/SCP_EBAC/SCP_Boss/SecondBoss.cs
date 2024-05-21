using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Audio;

namespace Boss
{
    public class SecondBoss : BossBase
    {
        [Header("Boss Attack")]
        private Coroutine _atkRoutine;
        public bool slam;
        public float damage = .5f;

        [Header("Anim")]
        private Vector3 defaultScale;
        public ParticleSystem VFX_Slam;
        public float duration;

        void Start()
        {
            defaultScale = transform.localScale;
        }

        void Update()
        {
            if(!dead)
            {
                CheckPlayer();
                Slam();
                if(playerDetected && !slam)
                {
                    LookPlayer();
                }
                
                if(playerDetected && !atkState)
                    SwitchAttack();

                if(!playerDetected && !walkState)
                {
                    SwitchWalk();
                }
            }

        }

        #region Atk
        public override IEnumerator AttackRoutine(Action action)
        {
            yield return new WaitForSeconds(timeToAtk);
            IdleAnim();

            int atk = 0;
            while(attackTimes > atk)
            {
                AnimAtk();
                yield return new WaitForSeconds(duration);

                if(SFX_boss != null) SFXManager.Instance.SetAudioByType(Audio.SFXType.BOSS_LAND, SFX_boss);
                atk++;
                Vector3 t = _player.transform.position + Vector3.up * 10;
                while(Vector3.Distance(transform.position, t) > .1f)
                {
                    transform.position = Vector3.Lerp(transform.position, t, speed * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }

                yield return new WaitForSeconds(.1f);
                slam = true;  
                yield return new WaitForSeconds(timeBtwAttack);
            }

            action?.Invoke();
        }

        IEnumerator DamageRoutine()
        {
            while(true)
            {
                _player.health.Damage(damage);
                yield return new WaitForSeconds(1);
            }
        }

        void Slam()
        {
            if(slam)
                transform.Translate(Vector3.down * 50 * Time.deltaTime);
        }

        void AnimAtk()
        {
            VFX_Slam?.Play();

            transform.DOScaleY(1, duration).SetEase(ease).OnComplete(
                delegate{transform.DOScaleY(defaultScale.y, duration/2).SetEase(ease);});
        }
        #endregion

        #region Collision

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Ground") && slam)
            {
                slam = false;
                AnimAtk();
                if(SFX_boss != null) SFXManager.Instance.SetAudioByType(Audio.SFXType.BOSS_LAND, SFX_boss);
            }

            if(other.gameObject.CompareTag("Player"))
            {
                _atkRoutine = StartCoroutine(DamageRoutine());
            }
        }

        void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                StopCoroutine(_atkRoutine);
            }
        }
        #endregion
    }
}
