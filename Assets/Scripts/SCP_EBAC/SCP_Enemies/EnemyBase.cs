using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Anim;
using DG.Tweening;
using Audio;
using Ebac.StateMachine;

namespace Enemy
{
    public enum EnemyState
    {
        WALK,
        ATTACK
    }

    public class EnemyBase : MonoBehaviour
    {
        public Collider enemyCollider;
        public FlashColor flashColor;
        public ParticleSystem hurtPS;
        public MeshRenderer mesh;
        public AudioSource SFX_enemy;

        [Header("States")]
        public StateMachine<EnemyState> stateMachine;
        public bool walkState;
        public bool attackState;

        [Header("Combat")]
        public HealthBase enemyHealth;
        public int enemyDamage = 1;
        protected bool dead;

        [Header("Movement")]
        public float speed = 5;
        public float turnSpeed = 5; 
        

        [Header("Waypoint")]
        public List<Transform> waypoint;
        public int _index;
        protected float _minDistance = 1;
        
        [Header("Animation")]
        [SerializeField] protected AnimationEnemy anim;
        
        //Spawn Animation
        public bool spawnAnim_b = true;
        public float startAnimDuration = 1;
        protected Ease ease = Ease.Linear; 


        [Header("Detect Player")]
        //References
        public MyPlayer _player;
        private string _playerTag = "Player";
        public LayerMask playerMask;
        public bool playerDetected;
        public float radius;


        // Start is called before the first frame update
        protected virtual void Start()
        {
            Init();
        }

        protected virtual void Update()
        {
            stateMachine.Update();
            CheckPlayer();
            
            if(!dead)
            { 
                if(!playerDetected && !walkState && waypoint.Count > 0)
                {
                    SwitchWalk();
                }

                //Can't Use the SwithAttack here Because of Enemy Bomb 
            }
        }

        public virtual void EnemyUpdate()
        {}

        protected virtual void Init()
        {
            SetPlayer();
            SetHealthActions();
            InitStateMachine();
            
        }

        #region  StateMachine
        void InitStateMachine()
        {
            stateMachine = new StateMachine<EnemyState>();
            stateMachine.Init();

            stateMachine.RegisterStates(EnemyState.WALK, new EnemyWalkState());
            stateMachine.RegisterStates(EnemyState.ATTACK, new EnemyAttackState());
        }

        public void SwitchWalk()
        {
            stateMachine.SwitchState(EnemyState.WALK, this);
        }

        public void SwitchAttack()
        {
            stateMachine.SwitchState(EnemyState.ATTACK, this);
        }

        public virtual void ExitState()
        {}
        #endregion

        #region Life and Death
        void SetHealthActions()
        {
            if(enemyHealth != null)
            {
                enemyHealth.onKill += OnEnemyKill;  
                enemyHealth.onDamage += OnEnemyDamage;
            } 
        } 

        protected void OnEnemyDamage(HealthBase health)
        {
            if(hurtPS != null) hurtPS.Emit(30);
            if(flashColor != null) flashColor.Flash();
            if(SFX_enemy != null) SFXManager.Instance.SetAudioByType(Audio.SFXType.PLAYER_HURT, SFX_enemy);
        }

        protected virtual void OnEnemyKill()
        {
            OnDeath();
            Invoke(nameof(Death), 1);
            if(stateMachine.currState != null) stateMachine.currState.OnStateExit();
            SetAnimByType(AnimEnemyType.DEATH);
            if(SFX_enemy != null) SFXManager.Instance.SetAudioByType(Audio.SFXType.ENEMY_DEATH, SFX_enemy);
            dead = true;
            enemyHealth.onKill -= OnEnemyKill;
            enemyHealth.onDamage -= OnEnemyDamage;
        }

        public virtual void OnDeath()
        {

        }

        public virtual void Death()
        {
            enemyCollider.enabled = false;
            mesh.enabled = false;
            foreach(var sprites in transform.GetComponentsInChildren<SpriteRenderer>())
                sprites.enabled = false;
        }
        #endregion

        #region  Animation
        public void SetAnimByType(AnimEnemyType currType)
        {
            if(anim != null)
                anim.SetAnimByType(currType);
        }

        private void SpawnAnim()
        {
            if(spawnAnim_b)
                transform.DOScale(0, startAnimDuration).SetEase(ease).From();
        }
        #endregion

        #region  Movement

        public virtual void Movement(Action action = null)
        {
            StartCoroutine(MovementRoutine(action));
        }

        public virtual void ActionMovement()
        {

        }

        IEnumerator MovementRoutine(Action action)
        {
            _index++;
            if(_index >= waypoint.Count) _index = 0;

            var pos = new Vector3(waypoint[_index].position.x, transform.position.y, waypoint[_index].position.z);
            while(Vector3.Distance(transform.position, waypoint[_index].position) > _minDistance)
            {
                LookDir();
                transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(1);
            action?.Invoke();
        }

        public void LookDir()
        {
                var lookDir = waypoint[_index].position - new Vector3(transform.position.x, waypoint[_index].position.y, transform.position.z);
                transform.forward = Vector3.Slerp(transform.forward, lookDir.normalized, Time.deltaTime * turnSpeed);
        }
        #endregion

        #region Attack
        public virtual void Attack(Action action = null)
        {
            StartCoroutine(ExplodeRoutine(action));
        }

        public virtual void ActionAttack()
        {}

        public virtual IEnumerator ExplodeRoutine(Action action)
        {
            yield return new WaitForSeconds(1);
            action?.Invoke();
        }
        #endregion

        #region  Player

        public void LookPlayer()
        {
            if(_player != null)
            {
                var playerPos = _player.transform.position - new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);
                transform.forward = Vector3.Slerp(transform.forward, playerPos, turnSpeed * Time.deltaTime);
            }
        }

        void SetPlayer()
        {
            _player = GameObject.FindGameObjectWithTag(_playerTag).GetComponent<MyPlayer>();
        }

        public void ChasePlayer()
        {
            LookPlayer();
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        #endregion

        #region  Detect Player

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public void CheckPlayer()
        {
            var hit = Physics.OverlapSphere(transform.position, radius, playerMask);

            if(hit.Length != 0)
                playerDetected = true;
            else
                playerDetected = false;
        }
        #endregion

        void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                _player.health.Damage(enemyDamage);
            }
        }
    }
}
