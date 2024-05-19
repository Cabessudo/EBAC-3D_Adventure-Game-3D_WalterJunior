using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;
using Ebac.StateMachine;

public class GameManager : Singleton<GameManager>
{
    public enum GameStates
    {
        Intro,
        Game,
        Win,
        Lose
    }

    public StateMachine<GameStates> stateMachine;


    void Start()
    {
        Init();
    }

    void Init()
    {
        stateMachine = new StateMachine<GameStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(GameStates.Intro, new GMStateIntro());
        stateMachine.RegisterStates(GameStates.Game, new StateBase());
        stateMachine.RegisterStates(GameStates.Win, new StateBase());
        stateMachine.RegisterStates(GameStates.Lose, new StateBase());

        stateMachine.SwitchState(GameStates.Intro, this);
    }
}
