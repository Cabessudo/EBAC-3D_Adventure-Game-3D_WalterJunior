using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunShootLimt : GunBase
{
    public List<UIGunUpdater> gunUIUpdaters;

    public int maxShoot = 20;
    public float timeToReload = 1;

    private int _currShoot;
    private bool _isReloading;

    void Awake()
    {
        GetAllUIs();
    }

    void Update()
    {
        UpdateUI();
    }

    protected override IEnumerator ShootRoutine()
    {
        while(true)
        {
            if(_isReloading) yield break;

            if(_currShoot < maxShoot)
            {
                Shoot();
                _currShoot++;
                UpdateUI();
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
            gunUIUpdaters.ForEach(i => i.UpdateValue(time/timeToReload));
            yield return new WaitForEndOfFrame();
        }
        _currShoot = 0;
        _isReloading = false;
    }

    void UpdateUI()
    {
        gunUIUpdaters.ForEach(i => i.UpdateValue(_currShoot, maxShoot));
    }

    void GetAllUIs()
    {
        gunUIUpdaters = GameObject.FindObjectsOfType<UIGunUpdater>().ToList();
    }
}
