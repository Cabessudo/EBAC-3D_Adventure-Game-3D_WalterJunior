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
        public WallAndFloorCheck wallAndFloorCheck;
        private Coroutine _movementRoutine;
        private Tween _currTween;
        public float timeToTurn = 1;
        public float timeStop = 2;
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

                if(!playerDetected && wallAndFloorCheck.check && !walkState)
                {
                    SwitchWalk();
                }

                if(playerDetected && CheckPlayerClose())
                {
                    SwitchAttack();
                }

            }
        }

        public override void EnemyUpdate()
        {
            if(playerDetected && !CheckPlayerClose())
            {
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
                _currTween = transform.DORotate(Vector3.up * turnSpeed, patrolDuration).SetEase(ease).SetRelative();
        }

        void Move()
        {
            if(_currTween != null)_currTween.Kill();

            if(!dead)
                _currTween = transform.DOMove(transform.forward * speed, patrolDuration).SetEase(ease).SetRelative();
        }

        void TurnAround()
        {
            if(!wallAndFloorCheck.check)
            transform.Rotate(Vector3.right * turnSpeed * Time.deltaTime);
        }
        #endregion

        #region Check
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
