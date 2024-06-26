using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class HealthBase : MonoBehaviour, IDamageable
{
    protected Rigidbody _rb;
    public float maxLife = 5;
    public float currLife;
    public float damageMultiply = 1;
    public bool destroyOnDeath;
    public bool canHit = true;

    public Action onKill;
    public Action<HealthBase> onDamage;

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        RestartLife();
        _rb = GetComponent<Rigidbody>();
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
            Destroy(gameObject, 2);

        onKill?.Invoke();
    }

    public virtual void TakeDamage(float damage)
    {
        if(currLife <= 0) return;
        
        if(canHit)
        {
            currLife -= damage * damageMultiply;
            onDamage?.Invoke(this);
        }

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
        _rb?.AddForce(dir * force, ForceMode.Impulse);
    }
}
