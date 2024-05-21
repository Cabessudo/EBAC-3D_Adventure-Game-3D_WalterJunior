using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Audio;

public class GunBase : MonoBehaviour
{
    [Header("Shoot Parameters")]
    public float shootSpeed;
    public float timeBtwShoots;
    public float timeToShoot;

    [Header("Shoot")]
    protected Coroutine _currRoutine;
    public GameObject shootObj;
    public Transform shootPos;
    public AudioSource SFX_shoot;
    
    void Start()
    {
        Init();
    }

    protected virtual void Init(){}

    protected virtual void Shoot()
    {
        var shoot = Instantiate(shootObj, shootPos);
        shoot.transform.parent = null;

        var projectile = shoot.GetComponent<ProjectileBase>();
        if(projectile != null)
        {
            projectile.speed = shootSpeed;
            projectile.DestroyShoot();
        }

        if(SFX_shoot != null) SFXManager.Instance?.SetAudioByType(Audio.SFXType.PLAYER_GUN, SFX_shoot);
    }

    protected virtual IEnumerator ShootRoutine(Action action)
    {
        yield return new WaitForSeconds(timeToShoot);
        while(true)
        {
            Shoot();
            action?.Invoke();
            yield return new WaitForSeconds(timeBtwShoots);
        }
    }
    public virtual void SFXShoot()
    {
        if(SFX_shoot != null) SFXManager.Instance?.SetAudioByType(Audio.SFXType.PLAYER_GUN, SFX_shoot);
    }

    public virtual void StartShoot(Action action = null)
    {
        StopShoot();
        _currRoutine = StartCoroutine(ShootRoutine(action));
        SFXShoot();
    }

    public virtual void StopShoot()
    {
        if(_currRoutine != null)
            StopCoroutine(_currRoutine);
    }

    protected virtual void OnDestroy()
    {
        if(_currRoutine != null) StopCoroutine(_currRoutine);
    }
}