using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test
{
    public enum States
    {
        Idle
    }

    void SetStates()
    {
        NewStateMachine<States> stateMachine = new NewStateMachine<States>();

        stateMachine.RegisterStates(States.Idle, new StateBase()); 
    }
}

//Where <T> is equal to Enum | it just transforms the T in Enum
public class NewStateMachine<T> where T : System.Enum
{
    public Dictionary<T, StateBase> dictionary;
    private StateBase _currState;
    public Player player;

    public void RegisterStates(T enumType, StateBase state)
    {
        dictionary.Add(enumType, state);
    }

    public void Init()
    {
        dictionary = new Dictionary<T, StateBase>();

    }

    void SwitchStates(T state)
    {
        if(_currState != null) _currState.OnStateExit();

        _currState = dictionary[state];
        _currState.OnStateEnter(player);
    }

    void Update()
    {
        if(_currState != null) _currState.OnStateStay();
    }
}
