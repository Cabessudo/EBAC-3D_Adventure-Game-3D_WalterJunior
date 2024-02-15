using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : MonoBehaviour
{
    public virtual void OnStateEnter(object o = null)
    {
        Debug.Log("OnStateEnter");
    }

    public virtual void OnStateStay()
    {
        Debug.Log("OnStateStay");
    }

    public virtual void OnStateExit()
    {
        Debug.Log("OnStateExit");
    }
}

public class StateIdle : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        base.OnStateEnter(o);
        var player = (Player)o;
        player.walk = false;
        player.jump = false;
        player.dead = false;
    }
}

public class StateWalk : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        base.OnStateEnter(o);
        var player = (Player)o;
        player.walk = true;
        player.jump = false;
        player.dead = false;
    }
}

public class StateJump : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        base.OnStateEnter(o);
        var player = (Player)o;
        player.jump = true;
        player.dead = false;
        player.walk = false;
    }
}

public class StateDead : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        base.OnStateEnter(o);
        var player = (Player)o;
        player.dead = true;
        player.walk = false;
        player.jump = false;
    }
}
