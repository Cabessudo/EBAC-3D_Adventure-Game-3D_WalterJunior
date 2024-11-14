using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Enemy;
using System;

public class EnemyDig : EnemyBase
{
    [Header("Dig")]
    public ParticleSystem PS_moveDig;
    public Ease easeDig;
    public float digDuration = 1;
    public float digDepth = -4;
    private bool _digged;

    //Attack Variables
    public float timeToAttack = 1;
    public float chaseTime;

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        CheckHeight();

        if(playerDetected && !attackState)
        {
            SwitchAttack();
        }

        if(_digged && !playerDetected)
            DigAttack();
    }

    public override void EnemyUpdate()
    {
        if(_digged)
        {
            ChasePlayer();
        }

        if(_digged && CheckPlayerAbove())
        {
            DigAttack();
        }
    }

    public override void ExitState()
    {
        if(_digged) DigAttack();
    }

    #region Attack
    public override void Attack(Action action = null)
    {
        StartCoroutine(AttackPlayerRoutine());
    }

    IEnumerator AttackPlayerRoutine()
    {
        Dig();
        yield return new WaitForSeconds(chaseTime);
        DigAttack();
    }

    [NaughtyAttributes.Button]
    void Dig()
    {
        transform.DOLocalMoveY(digDepth, digDuration).SetDelay(timeToAttack).SetEase(easeDig).SetRelative().OnComplete(
            delegate
            {
                PS_moveDig.Play();
                _digged = true;
            });
    }

    [NaughtyAttributes.Button]
    void DigAttack()
    {
        attackState = false;
        _digged = false;
        PS_moveDig.Stop();
        transform.DOLocalMoveY(-digDepth, digDuration).SetEase(easeDig).SetRelative();
    }

    bool CheckPlayerAbove()
    {
        return Physics.Raycast(transform.position, Vector3.up, 15, playerMask);
    }

    void CheckHeight()
    {
        if(transform.position.y > 3)
        {
            transform.position = new Vector3(transform.position.x, 3, transform.position.z);
            transform.DOKill();
        }
    }
    #endregion

    #region Collision

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            var dir = _player.transform.position - transform.position;
            _player.health.Damage(1, dir.normalized, 25);
        }
    }
    #endregion
}
