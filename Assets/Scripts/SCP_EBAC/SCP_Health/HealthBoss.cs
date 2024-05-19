using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBoss : HealthPlayer
{
    [Header("Events")]
    public UnityEvent killReward;

    protected override void Init()
    {
        RestartLife();
        _rb = GetComponent<Rigidbody>();
    }

    protected override void Kill()
    {
        base.Kill();
        killReward?.Invoke();
    }
}
