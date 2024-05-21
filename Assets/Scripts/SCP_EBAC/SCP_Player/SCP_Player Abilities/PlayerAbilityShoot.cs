using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<GunBase> gunList;
    public GunBase _currGun;

    public Transform spawnPos; 
    public FlashColor flash;

    void Update()
    {
        if(MyPlayer.Instance.isAlive)
        {
            if(MyPlayer.Instance.canChangeGun && MyPlayer.Instance.flamethrower)
            {
                GetGun(2);
            }

            if(MyPlayer.Instance.canChangeGun && MyPlayer.Instance.shotgun)
            {
                GetGun(1);
            }

            if(MyPlayer.Instance.canChangeGun && !MyPlayer.Instance.shotgun && !MyPlayer.Instance.flamethrower)
            {
                GetGun();
            }
        }
    }

    protected override void Init()
    {
        base.Init();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => StopShoot();
    }

    void GetGun(int i = 0)
    {
        if(_currGun != null) Destroy(_currGun.gameObject, .1f);

        _currGun = Instantiate(gunList[i], spawnPos);
        MyPlayer.Instance.canChangeGun = false;
    }

    private void StartShoot()
    {
        _currGun.StartShoot(flash.Flash);
        Debug.Log("Start Shoot");
    }

    private void StopShoot()
    {
        Debug.Log("Stop Shoot");
        _currGun.StopShoot();
    }
}
