using UnityEngine;
using Ebac.StateMachine;

public class PlayerState : StateBase
{
    protected Player player;

    public override void OnStateEnter(object o)
    {
        base.OnStateEnter(o);
        player = (Player)o;
    }
}

public class PlayerIdleState : PlayerState
{
    public override void OnStateEnter(object o)
    {
        Debug.Log("Idle State Enter");
        if(player == null) return;
        player.idle = true;
        player.walk = false;
    }

    public override void OnStateStay(object o)
    {
        Debug.Log("Idle State Stay");
        if(player == null) return;
    }
}

public class PlayerWalkState : PlayerState
{
    public override void OnStateStay(object o)
    {
        Debug.Log("Walk State Stay");
        if(player == null) return;
        player.Movement();
        player.idle = false;
    }
}

public class PlayerJumpState : PlayerState
{
    public override void OnStateEnter(object o)
    {
        Debug.Log("Jump State Enter");
        if(player == null) return;
        player.Invoke(nameof(player.Jump), .1f);
        player.isJumping = true;
        player.idle = false;
    }
}
