using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

public class PlayerAbilitiyCloths : PlayerAbilityBase
{
    public List<ClothItemBase> cloths;
    private ClothItemBase _currCloth;

    protected override void Init()
    {
        base.Init();

        cloths = SaveManager.Instance.setup.currCloths;
        inputs.Gameplay.ChangeToFirstCloth.performed += ctx => ActiveCloth();
        inputs.Gameplay.ChangeToSecondCloth.performed += ctx => ActiveCloth(1);
    }

    void ActiveCloth(int index = 0)
    {
        _currCloth = cloths[index];
        _currCloth.ActivePWUP();
    }
}
