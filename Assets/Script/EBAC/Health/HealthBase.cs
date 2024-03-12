using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBase : MonoBehaviour, IDamageable
{
    public int maxLife = 5;
    public int currLife;
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

    public virtual void RestartLife()
    {
        currLife = maxLife;
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
        currLife -= damage;
        onDamage?.Invoke(this);

        if(currLife <= 0)
        {
            Kill();
        }

    }

    public void Damage(int damage)
    {
        TakeDamage(damage);
    }

    public void Damage(int damage, Vector3 dir, float force)
    {
        Damage(damage); 
    }
}
