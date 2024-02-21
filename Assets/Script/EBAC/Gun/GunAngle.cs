using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAngle : GunShootLimt
{
    public int amountPerShoot = 4;
    public float angle = 15;

    protected override void Shoot()
    {
        int mult = 0;
        bool once = false;

        for(int i = 0; i < amountPerShoot; i++)
        {
            if(i%2 == 0)
                mult++;
            
            var shoot = Instantiate(shootObj, shootPos);
            if(i > 2 && i%3 == 0 && !once)
            {
                shoot.transform.localEulerAngles = Vector3.zero;
                once = true;
            }
            else
                shoot.transform.localEulerAngles = Vector3.zero + Vector3.up * (i%2 == 0 ? angle : -angle) * mult;
            

            shoot.transform.parent = null;

            var projectile = shoot.GetComponent<ProjectileBase>();
            if(projectile != null)
            {
                projectile.speed = shootSpeed;
                projectile.DestroyObj(1);
            }
        }
    }
}
