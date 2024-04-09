using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

public class ClothItemSpeed : ClothItemBase
{
    public float clothSpeed = 50;

    protected override void Collect()
    {
        base.Collect();
        MyPlayer.Instance.ChangeSpeed(clothSpeed, duration);
    }

    public override void ActivePWUP()
    {
        base.ActivePWUP();
        MyPlayer.Instance.ChangeSpeed(clothSpeed, duration);
    }
}
