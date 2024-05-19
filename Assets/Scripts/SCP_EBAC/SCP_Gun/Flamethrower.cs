using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class Flamethrower : GunBase
{
    [Header("Important Only Below")]
    public Collider flamethrowerCollider;
    public List<ParticleSystem> PS_flames;
    public float flamethrowerDamage = 1;
    public float giveDamageTime = 1;
    public float damagePerSec = 1;
    public bool _isOn;

    void Start()
    {
        TurnOff();
    }
    
    #region Shoot
    public override void StartShoot(Action action = null)
    {
        TurnOn();
    }

    public override void StopShoot()
    {
        TurnOff();
    }
    #endregion

    #region  Flamethrower
    void TurnOn()
    {
        _isOn = true;
        PS_flames.ForEach(i => i.Play());
        flamethrowerCollider.enabled = true;
        if(SFX_shoot != null) SFXManager.Instance?.SetAudioByType(Audio.SFXType.PLAYER_FLAMETHROWER, SFX_shoot);
    }

    void TurnOff()
    {
        _isOn = false;
        PS_flames.ForEach(i => i.Stop());
        flamethrowerCollider.enabled = false;
        SFX_shoot.Stop();
        if(_currRoutine != null) StopCoroutine(_currRoutine);
    }

    IEnumerator DamageRoutine(HealthBase enemyHealth)
    {
        yield return new WaitForSeconds(giveDamageTime);
        
        while(true)
        {
            enemyHealth.TakeDamage(flamethrowerDamage);
            yield return new WaitForSeconds(damagePerSec);
        }
    }
    #endregion

    #region Collider
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") && _isOn)
        {
            var enemyHealth = other.gameObject.GetComponent<HealthBase>();
            if(enemyHealth != null)
            {
                _currRoutine = StartCoroutine(DamageRoutine(enemyHealth));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            if(_currRoutine != null) StopCoroutine(_currRoutine);
        }
    }
    #endregion

    protected override void OnDestroy()
    {
        base.OnDestroy();
        TurnOff();
    }
}
