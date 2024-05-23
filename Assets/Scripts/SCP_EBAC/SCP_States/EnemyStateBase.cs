using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using Ebac.StateMachine;

public class EnemyStateBase : StateBase
{
    protected EnemyBase _enemy;

    public override void OnStateEnter(object o)
    {
        base.OnStateEnter(o);
        _enemy = (EnemyBase)o;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _enemy.StopAllCoroutines();
        _enemy.walkState = false;
        _enemy.attackState = false;
        Debug.Log("Enemy Exit");
    }
}

public class EnemyWalkState : EnemyStateBase
{
    public override void OnStateEnter(object o)
    {
        base.OnStateEnter(o);
        _enemy.Movement(Action);
        _enemy.walkState = true;
        Debug.Log("Enemy Walk Enter");
    }

    public override void OnStateStay(object o)
    {
        base.OnStateStay(o);
    }

    //To EnemyBase Cause Movement to Waypoints
    void Action()
    {
        _enemy.walkState = false;
    }
}

public class EnemyAttackState : EnemyStateBase
{
    public override void OnStateEnter(object o)
    {
        base.OnStateEnter(o);
        _enemy.attackState = true;
        _enemy.Attack(Action);
    }

    void Action()
    {
        _enemy.ActionAttack();
    }

    public override void OnStateStay(object o)
    {
        base.OnStateStay(o);
        _enemy.EnemyUpdate();
    }
}