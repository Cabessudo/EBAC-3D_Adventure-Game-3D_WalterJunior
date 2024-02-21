using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gun;
    public Transform gunPos;

    private GunBase _currGun;

    protected override void Init()
    {
        base.Init();
        GetGun();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => StopShoot();
    }

    private void GetGun()
    {
        _currGun = Instantiate(gun, gunPos);
        _currGun.transform.localPosition = _currGun.transform.eulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        _currGun.StartShoot();
        Debug.Log("Start Shoot");
    }

    private void StopShoot()
    {
        Debug.Log("Stop Shoot");
        _currGun.StopShoot();
    }
}
