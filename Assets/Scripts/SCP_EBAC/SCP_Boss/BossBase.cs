using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using Ebac.StateMachine;
using DG.Tweening;
using Anim;
using Audio;

namespace Boss
{
    public enum BossState
    {
        WALK,
        ATTACK,
        DEATH
    }
    
    public class BossBase : MonoBehaviour
    {
        [Header("States")]
        protected StateMachine<BossState> stateMachine;
        public bool atkState;
        public bool walkState;

        [Header("Walk")]
        public List<Transform> waypoints;
        public float speed;
        public float turnSpeed = 12;
        protected int _index;

        [Header("Attack Variables")]
        public float timeToAtk = 1;
        public int attackTimes = 5;
        public float timeBtwAttack = .5f;

        [Header("Animation")]
        [SerializeField] protected AnimationBase<AnimEnemyType> _bossAnim;
        protected bool _idleAnim;
        protected Ease ease = Ease.Linear;

        //Particles
        public ParticleSystem PS_bossDamage;

        [Header("Boss Health")]
        public GameObject bossHealthUI;
        public HealthBase bossHealth;
        public FlashColor bossFlashColor;
        protected bool dead; 

        [Header("SFX")]
        public AudioSource SFX_boss;
        
        [Header("Player References")]
        protected MyPlayer _player;
        public LayerMask playerMask;
        public bool playerDetected;
        public float radius;
        public float outOfRange = 20;

        

        protected virtual void Awake()
        {
            InitStates();
            SetHealthActions();
            SetPlayer();
        }

        #region  StateMachine
        void InitStates()
        {
            stateMachine = new StateMachine<BossState>();
            stateMachine.Init();
            
            stateMachine.RegisterStates(BossState.WALK, new BossWalk());
            stateMachine.RegisterStates(BossState.ATTACK, new BossAttack());
            stateMachine.RegisterStates(BossState.DEATH, new BossDeath());
        }

        public void SwitchState(BossState state)
        {
            stateMachine.SwitchState(state, this);
        }

        #endregion

        #region  Debug

        [NaughtyAttributes.Button]
        public void SwitchWalk()
        {
            SwitchState(BossState.WALK);
        }

        [NaughtyAttributes.Button]
        public void SwitchAttack()
        {
            SwitchState(BossState.ATTACK);
        } 

        [NaughtyAttributes.Button]
        public void SwitchDeath()
        {
            SwitchState(BossState.DEATH);
        }
        #endregion

        #region Walk
        public virtual void Walk(Action action = null)
        {
            IdleAnim();
            var currIndex = UnityEngine.Random.Range(0, waypoints.Count);
            if(_index == currIndex) currIndex++;

            if(currIndex >= waypoints.Count) currIndex = 0;

            _index = currIndex;
            StartCoroutine(WalkRoutine(waypoints[_index], action));
        }

        public void IdleAnim()
        {
            if(!_idleAnim)
            {
                _bossAnim.SetAnimByType(AnimEnemyType.IDLE);
                _idleAnim = true;
                bossHealth.canHit = false;
            }
        }

        public void LookWayPoint()
        {
            var lookWayPointDir = waypoints[_index].position - new Vector3(transform.position.x, waypoints[_index].position.y, transform.position.z);
            transform.forward = Vector3.Slerp(transform.forward, lookWayPointDir.normalized, Time.deltaTime * turnSpeed);
        }

        IEnumerator WalkRoutine(Transform t, Action onAction)
        {
            while(Vector3.Distance(transform.position, t.position) > .1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(1);
            onAction?.Invoke();
        }
        #endregion

        #region Attack
        public virtual void Attack(Action action = null)
        {
            StartCoroutine(AttackRoutine(action));
        }

        public virtual IEnumerator AttackRoutine(Action onAction)
        {
            yield return null; 
        }

        [NaughtyAttributes.Button]
        public void Stunned()
        {
            _bossAnim.SetAnimByType(AnimEnemyType.STUNNED);
            _idleAnim = false;
            bossHealth.canHit = true;
        }

        #endregion

        #region Life & Death
        void SetHealthActions()
        {
            bossHealth.onKill += OnBossKill;
            bossHealth.onDamage += OnBossDamage;
        }

        void OnBossDamage(HealthBase health)
        {
            if(bossFlashColor != null) bossFlashColor.Flash();
            if(PS_bossDamage != null) PS_bossDamage.Play();
            if(SFX_boss != null) SFXManager.Instance.SetAudioByType(Audio.SFXType.ENEMY_HURT, SFX_boss);
        }

        void OnBossKill()
        {
            SwitchDeath();
            // if(SFX_boss != null) SFXManager.Instance.SetAudioByType(Audio.SFXType.ENEMY_HURT, SFX_boss);
            dead = true;
            _bossAnim.SetAnimByType(AnimEnemyType.DEATH);
            // bossHealthUI.SetActive(false); 
            // PS_bossDamage.Emit(50);
            bossHealth.onKill -= OnBossKill;
            bossHealth.onDamage -= OnBossDamage;
        }
        #endregion

       

        #region Player
        public void CheckPlayer()
        {
            var hit = Physics.OverlapSphere(transform.position, radius, playerMask);

            if(hit.Length > 0)
            {
                bossHealthUI.SetActive(true);
                playerDetected = true;
            }

            if(Vector3.Distance(_player.transform.position, transform.position) > outOfRange)
            {
                bossHealthUI?.SetActive(false);
                playerDetected = false;
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public void LookPlayer()
        {
            var lookPlayerDir = _player.transform.position - new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);
            transform.forward = Vector3.Slerp(transform.forward, lookPlayerDir.normalized, Time.deltaTime * turnSpeed);
        } 

        void SetPlayer()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<MyPlayer>();
        }

        #endregion
    }
}
