using UnityEngine;
using Ebac.StateMachine;

public class PlayerState
{
    public class IdleState : StateBase
    {
        public override void OnStateEnter(object o = null)
        {
            Debug.Log("Idle State Enter");
            var player = (Player)o;
            if(player == null) return;
            player.idle = true;
            player.walk = false;
        }

        public override void OnStateStay(object o = null)
        {
            Debug.Log("Idle State Stay");
            var player = (Player)o;
            if(player == null) return;
            player.Rotation();
        }
    }

    public class WalkState : StateBase
    {
        public override void OnStateStay(object o = null)
        {
            Debug.Log("Walk State Stay");
            var player = (Player)o;
            if(player == null) return;
            player.Movement();
            player.Rotation();
            player.idle = false;
        }
    }

    public class JumpState : StateBase
    {
        public override void OnStateEnter(object o = null)
        {
            Debug.Log("Jump State Stay");
            var player = (Player)o;
            if(player == null) return;
            player.Jump();
            player.jump = false;
            player.idle = false;
        }
    }
}
