using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBase : MonoBehaviour
{
    public int maxLife = 5;
    private int _currLife;
    public bool destroyOnDeath;

    public Action onKill;
    public Action<HealthBase> onDamage;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        RestartLife();
    }

    private void RestartLife()
    {
        _currLife = maxLife;
    }

    [NaughtyAttributes.Button]
    void Damage()
    {
        TakeDamage(5);
    }

    private void Kill()
    {
        if(destroyOnDeath)
            Destroy(gameObject, 1);

        onKill?.Invoke();
    }


    public virtual void TakeDamage(int damage)
    {
        _currLife -= damage;
        // if(flashColor != null)
        //     flashColor.Flash();

        // if(hurtPS != null)
        //     hurtPS.Emit(15);

        if(_currLife <= 0)
        {
            Kill();
        }

        onDamage?.Invoke(this);
    }
}
