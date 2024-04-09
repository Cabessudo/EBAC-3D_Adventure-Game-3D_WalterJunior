using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using Ebac.StateMachine;
using DG.Tweening;

namespace Boss
{
    public enum BossState
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }
    
    public class BossBase : MonoBehaviour
    {
        [Header("States")]
        private StateMachine<BossState> stateMachine;
        public bool atkState;
        public bool walkState;

        [Header("Walk")]
        public List<Transform> waypoints;
        public float speed;
        public float turnSpeed = 12;
        private int _index;

        [Header("Attack")]
        public Transform bossShootPos;
        public GameObject bossShoot;
        public float shootHeight = 2;
        public float timeToAtk = 1;
        public int attackTimes = 5;
        public float timeBtwAttack = .5f;

        [Header("Animation")]
        private Ease easeSpawn = Ease.Linear;
        public float durationSpawn = .2f;
        //Particles
        public ParticleSystem bossParticle;

        [Header("Boss Health")]
        public HealthBase bossHealth;
        
        [Header("Events")]
        public Event onKill;
        public UnityEvent killReward;

        [Header("Player References")]
        protected MyPlayer _ebacPlayer;
        protected MyPlayer _player;
        public LayerMask playerMask;
        public bool playerDetected;
        public float radius;
        public float outOfRange = 20;

        void Awake()
        {
            InitStates();
            SetHealthActions();
            SetPlayer();
        }

        void Update()
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
            else if(!playerDetected && walkState)
                LookWayPoint();

        }


        #region  StateMachine
        void InitStates()
        {
            stateMachine = new StateMachine<BossState>();
            stateMachine.Init();
            
            stateMachine.RegisterStates(BossState.INIT, new BossInit());
            stateMachine.RegisterStates(BossState.IDLE, new BossIdle());
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
        public void Spawn()
        {
            SwitchState(BossState.INIT);
        }

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
        #endregion

        #region Animation
        public void AnimSpawn()
        {
            transform.DOScale(0, durationSpawn).SetEase(easeSpawn).From();
        }
        #endregion

        #region Walk
        public void Walk(Action action = null)
        {
            var currIndex = UnityEngine.Random.Range(0, waypoints.Count);
            if(_index == currIndex) currIndex++;

            if(currIndex >= waypoints.Count) currIndex = 0;

            _index = currIndex;
            StartCoroutine(WalkRoutine(waypoints[_index], action));
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
        public void Attack(Action action = null)
        {
            StartCoroutine(AttackRoutine(action));
        }

        IEnumerator AttackRoutine(Action onAction)
        {
            yield return new WaitForSeconds(timeToAtk);

            int atk = 0;
            while(attackTimes > atk)
            {
                atk++;
                transform.DOScale(1.2f, .1f).SetLoops(2, LoopType.Yoyo);
                Shoot();
                yield return new WaitForSeconds(timeBtwAttack);
            }

            onAction?.Invoke();
        }
        #endregion

        #region Life & Death
        void SetHealthActions()
        {
            bossHealth.onKill += OnBossKill;
        }

        public void Death()
        {
            bossParticle.Emit(50);
        }

        void OnBossKill()
        {
            SwitchState(BossState.DEATH);
            killReward?.Invoke();
            bossHealth.onKill -= OnBossKill;
        }
        #endregion

        #region  BossShoot

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

        #region Player
        void CheckPlayer()
        {
            var hit = Physics.OverlapSphere(transform.position, radius, playerMask);

            if(hit.Length > 0)
                playerDetected = true;

            if(Vector3.Distance(_player.transform.position, transform.position) > outOfRange)
                playerDetected = false;
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
