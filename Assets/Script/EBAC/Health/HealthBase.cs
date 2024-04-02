using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float maxLife = 5;
    public float currLife;
    public float damageMultiply = 1;
    public bool destroyOnDeath;

    public Action onKill;
    public Action<HealthBase> onDamage;

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        RestartLife();
    }

    public virtual void RestartLife()
    {
        currLife = maxLife;
    }

    [NaughtyAttributes.Button]
    void Damage()
    {
        TakeDamage(1);
    }

    protected virtual void Kill()
    {
        if(destroyOnDeath)
            Destroy(gameObject, 1);

        onKill?.Invoke();
    }

    public virtual void TakeDamage(float damage)
    {
        if(currLife <= 0) return;
        
        currLife -= damage * damageMultiply;
        onDamage?.Invoke(this);

        if(currLife <= 0)
        {
            Kill();
        }

    }

    public void Damage(float damage)
    {
        TakeDamage(damage);
    }

    public void Damage(float damage, Vector3 dir, float force)
    {
        Damage(damage); 
    }
}
