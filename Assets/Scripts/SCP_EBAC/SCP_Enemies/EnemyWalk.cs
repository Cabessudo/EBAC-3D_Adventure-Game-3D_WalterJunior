using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace Enemy
{
    public class EnemyWalk : EnemyShoot
    {
        [Header("Patrol")]
        public WallCheck wallCheck;
        private Coroutine _movementRoutine;
        private Tween _currTween;

        //Movement
        public float moveDuration = 5;
        public float stopTime = 2;

        //Patrol
        protected bool patrol_b = true;
        public float patrolDuration = 1;

        [Header("ChasePlayer")]
        public bool closeToPlayer;
        public float _minDis = 5;

        protected override void Update()
        {
            stateMachine.Update();
            CheckPlayer();

            if(!dead)
            {
                TurnAround();

                if(!playerDetected && !wallCheck.check && !walkState)
                {
                    SwitchWalk();
                }

                if(playerDetected && CheckPlayerClose() && !wallCheck.check && !attackState)
                {
                    SwitchAttack();
                }

            }
        }

        public override void EnemyUpdate()
        {
            LookPlayer();

            if(playerDetected && !CheckPlayerClose())
            {
                if(attackState) PlayerAway();
                ChasePlayer();
            }
        }

        #region  Movement
        public override void Movement(Action action = null)
        {
            _movementRoutine = StartCoroutine(StartPatrol());
        }

        IEnumerator StartPatrol()
        {
            while(true)
            {
                if(patrol_b)
                {
                    Patrol();
                    yield return new WaitForSeconds(patrolDuration);
                    Move();
                    yield return new WaitForSeconds(moveDuration);
                    patrol_b = false;
                }
                else
                {
                    yield return new WaitForSeconds(stopTime);
                    patrol_b = true;
                }
            }
        }

        void Patrol()
        {
            if(_currTween != null)_currTween.Kill();

            if(!dead)
                _currTween = transform.DORotate(Vector3.up * turnSpeed, patrolDuration).SetEase(ease).SetRelative();
        }

        void Move()
        {
            if(_currTween != null)_currTween.Kill();

            if(!dead)
                _currTween = transform.DOMove(transform.forward * speed, moveDuration).SetEase(ease).SetRelative();
        }

        void TurnAround()
        {
            if(wallCheck.check)
            {
                if(stateMachine.currState != null) stateMachine.currState.OnStateExit();
                transform.Rotate(Vector3.right * turnSpeed * Time.deltaTime);
            }
        }
        #endregion

        #region Check
        void PlayerAway()
        {
            attackState = false;
            enemyGun.StopAllCoroutines();
            anim.SetAnimByType(Anim.AnimEnemyType.IDLE);
        }

        bool CheckPlayerClose()
        {
            if(Vector3.Distance(_player.transform.position, transform.position) > _minDis)
                return false;
            else 
                return true;
        }
        #endregion

        
    }
}
