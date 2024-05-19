using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class GunShootLimt : GunBase
{
    public int maxShoot = 20;
    public float timeToReload = 1;

    private int _currShoot;
    private bool _isReloading;

    protected override IEnumerator ShootRoutine(Action action)
    {
        while(true)
        {
            if(_isReloading) yield break;

            if(_currShoot < maxShoot)
            {
                Shoot();
                _currShoot++;
                CheckShoots();
                yield return new WaitForSeconds(timeBtwShoots);
            }
        }
    }

    void CheckShoots()
    {
        if(_currShoot >= maxShoot)
        {
            StopShoot();
            _isReloading = true;
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        float time = 0;
        while(time < timeToReload)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _currShoot = 0;
        _isReloading = false;
    }
}
