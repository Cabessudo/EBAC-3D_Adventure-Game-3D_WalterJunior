using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public class FSMExample : MonoBehaviour
{
    public StateMachine<States> stateMachine;

    public enum States
    {
        None,
        One
    }

    void Start()
    {
        stateMachine = new StateMachine<States>();
        stateMachine.Init();
        stateMachine.RegisterStates(States.None, new StateBase());
        stateMachine.RegisterStates(States.One, new StateBase());
    }
}
