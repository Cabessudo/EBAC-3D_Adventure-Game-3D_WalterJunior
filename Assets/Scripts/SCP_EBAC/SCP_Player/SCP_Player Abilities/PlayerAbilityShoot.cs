using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<GunBase> guns;
    public Transform gunPos;

    public FlashColor flash;

    private GunBase _currGun;

    protected override void Init()
    {
        base.Init();
        GetGun();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => StopShoot();

        inputs.Gameplay.ChangeToFirstGun.performed += ctx => GetGun();
        inputs.Gameplay.ChangeToSecondGun.performed += ctx => GetGun(1);
    }

    private void GetGun(int i = 0)
    {
        if(_currGun != null && _currGun != guns[i]) Destroy(_currGun.gameObject);

        _currGun = Instantiate(guns[i], gunPos);
        _currGun.transform.localPosition = Vector3.zero;

        // _currGun.transform.localPosition = _currGun.transform.eulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        _currGun.StartShoot();
        flash?.Flash();
        Debug.Log("Start Shoot");
    }

    private void StopShoot()
    {
        Debug.Log("Stop Shoot");
        _currGun.StopShoot();
    }
}
