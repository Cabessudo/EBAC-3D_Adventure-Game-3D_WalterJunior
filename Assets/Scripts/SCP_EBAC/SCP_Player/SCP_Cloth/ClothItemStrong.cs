using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

public class ClothItemStrong : ClothItemBase
{
    public float damageMultiply = .5f;

    protected override void Collect()
    {
        base.Collect();
        MyPlayer.Instance.health.ChangeDamage(damageMultiply, duration);
    }

    public override void ActivePWUP()
    {
        base.ActivePWUP();
        MyPlayer.Instance.health.ChangeDamage(damageMultiply, duration);
    }
}
