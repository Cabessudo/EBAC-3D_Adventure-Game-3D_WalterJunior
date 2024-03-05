using UnityEngine;
using Ebac.StateMachine;

public class PlayerState : StateBase
{
    protected Player player;

    public override void OnStateEnter(params object[] o)
    {
        base.OnStateEnter(o);
        player = (Player)o[0];
    }
}

public class PlayerIdleState : PlayerState
{
    public override void OnStateEnter(params object[] o)
    {
        Debug.Log("Idle State Enter");
        if(player == null) return;
        player.idle = true;
        player.walk = false;
    }

    public override void OnStateStay(params object[] o)
    {
        Debug.Log("Idle State Stay");
        if(player == null) return;
        player.Rotation();
    }
}

public class PlayerWalkState : PlayerState
{
    public override void OnStateStay(params object[] o)
    {
        Debug.Log("Walk State Stay");
        if(player == null) return;
        player.Movement();
        player.Rotation();
        player.idle = false;
    }
}

public class PlayerJumpState : PlayerState
{
    public override void OnStateEnter(params object[] o)
    {
        Debug.Log("Jump State Stay");
        if(player == null) return;
        player.Jump();
        player.jump = false;
        player.idle = false;
    }
}
