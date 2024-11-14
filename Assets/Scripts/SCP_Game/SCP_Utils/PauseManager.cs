using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;

public class PauseManager : Singleton<PauseManager>
{
    public bool pause = false;

    public void Enter()
    {
        Time.timeScale = 0;
        pause = true;
    }

    public void Exit()
    {
        Time.timeScale = 1.0f;
        pause = false;
    }
}
