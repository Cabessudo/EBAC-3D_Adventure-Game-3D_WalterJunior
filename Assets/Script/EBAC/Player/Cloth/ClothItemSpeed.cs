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
        EbacPlayer.Instance.ChangeSpeed(clothSpeed, duration);
    }
}
