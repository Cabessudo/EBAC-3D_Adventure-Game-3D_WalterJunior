using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

namespace Boss
{

    public class BossStateBase : StateBase
    {
        protected BossBase boss;

        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss = (BossBase)objs[0];
            Debug.Log("Booooosssss");
        }
    }

    public class BossInit : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.AnimSpawn();
        }
    }

    public class BossIdle : BossStateBase
    {

    }

    public class BossWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            Debug.Log("Walk Enter");
            base.OnStateEnter(objs);
            boss.Walk(Action);
            boss.walkState = true;
            boss.atkState = false; 
        }

        public override void OnStateStay(params object[] o)
        {
            base.OnStateStay(o);
            Debug.Log("WALK STAY");
            boss.LookWayPoint();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            boss.StopAllCoroutines();
        }

        private void Action()
        {
            // boss.SwitchAttack();
            boss.walkState = false;
        }

    }

    public class BossAttack : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            Debug.Log("Atk Enter");
            base.OnStateEnter(objs);
            boss.Attack(Action);
            boss.walkState = false;
            boss.atkState = true;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            boss.StopAllCoroutines();
        }

        private void Action()
        {
            // boss.SwitchWalk();
            boss.atkState = false;
        }
    }

    public class BossDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.transform.localScale = Vector3.one * .2f;
            boss.Death();
            Debug.Log("Boss Death");
        }
    }
}
