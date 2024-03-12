using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Anim;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider enemyCollider;
        public FlashColor flashColor;
        public ParticleSystem hurtPS;

        [Header("Combat")]
        public HealthBase enemyHealth;
        public int enemyDamage = 1;
        private bool dead;

        [Header("Movement")]
        public float speed = 5;
        public float turnSpeed = 5; 
        public float timeToTurn = 1;
        public float timeStop = 2;
        protected bool patrol_b = true;
        
        [Header("Animation")]
        [SerializeField] protected AnimationBase anim;
        private Coroutine _movementRoutine;
        private Tween _currTween;
        public float patrolDuration = 1;

        [Header("Spawn Animation")]
        public bool spawnAnim_b = true;
        public float startAnimDuration = 1;
        private Ease easeLinear = Ease.Linear; //SetRelative()


        [Header("Detect Player")]
        //References
        protected EbacPlayer _player;
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
            CheckPlayer();
            
            if(!dead)
                Movement();
        }
        

        protected virtual void Init()
        {
            SpawnAnim();
            SetPlayer();
            SetHealthActions();
        }

        #region Life and Death
        void SetHealthActions()
        {
            if(enemyHealth != null)
            {
                enemyHealth.onKill += OnEnemyKill;  
                enemyHealth.onDamage += OnEnemyDamage;
            } 
        } 

        protected void OnEnemyKill()
        {
            SetAnimByType(AnimType.DEATH);
            if(enemyHealth != null)
            {
                enemyHealth.onKill -= OnEnemyKill;
                enemyHealth.onDamage -= OnEnemyDamage;
            }
        }

        protected void OnEnemyDamage(HealthBase health)
        {
            if(hurtPS != null) hurtPS.Emit(30);
            if(flashColor != null) flashColor.Flash();
        }

        public void Damage(int damage)
        {
            if(enemyHealth != null) enemyHealth.TakeDamage(damage);
        }

        public void Damage(int damage, Vector3 dir, float force)
        {
            Damage(damage);
            transform.DOMove(transform.position - dir, .15f);
        }
        #endregion

        #region  Animation
        public void SetAnimByType(AnimType currType)
        {
            if(anim != null)
                anim.SetAnimByType(currType);
        }

        private void SpawnAnim()
        {
            if(spawnAnim_b)
                transform.DOScale(0, startAnimDuration).SetEase(easeLinear).From();
        }

        #endregion

        #region  Movement

        protected virtual void Movement()
        {
            _movementRoutine = StartCoroutine(StartPatrol());
        }

        IEnumerator StartPatrol()
        {
            if(patrol_b)
            {
                Patrol();
                yield return new WaitForSeconds(timeToTurn);
                Move();
                yield return new WaitForSeconds(timeToTurn);
                patrol_b = false;
            }
            else
            {
                yield return new WaitForSeconds(timeStop);
                patrol_b = true;
            }
        }

        void Patrol()
        {
            if(_currTween != null)_currTween.Kill();

            if(!dead)
                _currTween = transform.DORotate(Vector3.up * turnSpeed, patrolDuration).SetEase(easeLinear).SetRelative();
        }

        void Move()
        {
            if(_currTween != null)_currTween.Kill();

            if(!dead)
                _currTween = transform.DOMove(transform.forward * speed, patrolDuration).SetEase(easeLinear).SetRelative();
        }

        #endregion

        #region  Player

        public void LookPlayer()
        {
            if(_player != null)
                transform.LookAt(_player.transform.position);
        }

        void SetPlayer()
        {
            if(GameObject.FindGameObjectWithTag(_playerTag) != null)
                _player = GameObject.FindGameObjectWithTag(_playerTag).GetComponent<EbacPlayer>();
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
                _player.playerHealth.Damage(enemyDamage);
            }
        }
    }
}
