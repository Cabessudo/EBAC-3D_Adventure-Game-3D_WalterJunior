using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public enum States
    {
        Idle,
        Walk,
        Jump,
        Dead
    }

    void SetStates()
    {
        StateMachine<States> stateMachine = new StateMachine<States>();
        stateMachine.RegisterStates(States.Idle, new StateIdle());
    }

}

public class StateMachine<T> where T : System.Enum
{
    public Dictionary<T, StateBase> dictionary;
    private StateBase _currState;
    public Player player;

    public StateBase currState
    {
        get { return _currState;}
    }


    public void Init()
    {
        dictionary = new Dictionary<T, StateBase>();
    }

    public void RegisterStates(T enumType, StateBase state)
    {
        dictionary.Add(enumType, state);
    }

    void SwitchState(T state)
    {
        if(_currState != null) _currState.OnStateExit();

        _currState = dictionary[state];
        _currState.OnStateEnter(player);
    }

    public void Update()
    {
        if(_currState != null) _currState.OnStateStay();
    }
}