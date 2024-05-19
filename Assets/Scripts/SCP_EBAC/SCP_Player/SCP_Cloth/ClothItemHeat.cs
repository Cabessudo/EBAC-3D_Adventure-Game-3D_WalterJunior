using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

public class ClothItemHeat : ClothItemBase
{

    public override void ActivePWUP()
    {
        base.ActivePWUP();
        MyPlayer.Instance.Flamethrower(duration);
    }
}
