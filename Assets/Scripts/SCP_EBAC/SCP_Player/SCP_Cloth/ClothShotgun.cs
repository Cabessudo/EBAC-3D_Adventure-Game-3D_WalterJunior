using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

public class ClothShotgun : ClothItemBase
{
    public override void ActivePWUP()
    {
        base.ActivePWUP();
        MyPlayer.Instance.Shotgun(duration);
    }
}
