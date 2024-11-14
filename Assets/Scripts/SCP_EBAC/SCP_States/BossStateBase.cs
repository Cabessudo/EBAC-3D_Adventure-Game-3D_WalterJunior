using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using Audio;

namespace Boss
{

    public class BossStateBase : StateBase
    {
        protected BossBase boss;
        
        public override void OnStateEnter(object obj)
        {
            boss = (BossBase)obj;
            // Debug.Log("Booooosssss");
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            boss.StopAllCoroutines();
            // Debug.Log("Exit Boss");
        }
    }

    public class BossWalk : BossStateBase
    {
        public override void OnStateEnter(object objs)
        {
            // Debug.Log("Walk Enter");
            base.OnStateEnter(objs);
            boss.Walk(Action);
            boss.walkState = true;
            boss.atkState = false; 
        }

        public override void OnStateStay(object o)
        {
            base.OnStateStay(o);
            // Debug.Log("WALK STAY");
            boss.LookWayPoint();
        }

        private void Action()
        {
            boss.walkState = false;
        }

    }

    public class BossAttack : BossStateBase
    {
        public override void OnStateEnter(object objs)
        {
            // Debug.Log("Atk Enter");
            base.OnStateEnter(objs);
            boss.Attack(Action);
            boss.walkState = false;
            boss.atkState = true;
        }

        private void Action()
        {
            boss.Stunned();
            boss.atkState = false;
            boss.timeToAtk = 5;
        }
    }

    public class BossDeath : BossStateBase
    {
        public override void OnStateEnter(object objs)
        {
            base.OnStateEnter(objs);
            boss.bossHealth.Damage(boss.bossHealth.currLife);
            //
            if(boss.SFX_boss != null) SFXManager.Instance.SetAudioByType(Audio.SFXType.ENEMY_DEATH, boss.SFX_boss);
            boss.bossHealthUI.SetActive(false); 
            boss.PS_bossDamage.Emit(25);
            // Debug.Log("Boss Death");
        }
    }
}
