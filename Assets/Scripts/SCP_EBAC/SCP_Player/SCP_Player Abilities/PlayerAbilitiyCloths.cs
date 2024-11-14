using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

public class PlayerAbilitiyCloths : PlayerAbilityBase
{
    private ClothSetup _currFirstCloth;
    private ClothSetup _currSecondCloth;
    public PlayerUI playerUI;

    protected override void Init()
    {
        base.Init();

        _currFirstCloth = SaveManager.Instance.firstCloth;
        _currSecondCloth = SaveManager.Instance.secondCloth;
        inputs.Gameplay.ChangeToFirstCloth.performed += ctx => ActiveFirstCloth();
        inputs.Gameplay.ChangeToSecondCloth.performed += ctx => ActiveSecondCloth();
    }

    void ActiveFirstCloth()
    {
        _currFirstCloth = SaveManager.Instance.firstCloth;

        if(SaveManager.Instance.setup.firstClothType != ClothType.NONE_First && playerUI.firstClothFill.canUse && playerUI.canChangeCloth) 
        {
            _currFirstCloth.cloth.ChangePWUP(SaveManager.Instance.firstCloth.tex, SaveManager.Instance.firstCloth.jetpackMat);
            playerUI.CheckFirstCloth(true);
            playerUI.ChangeMainCloth(SaveManager.Instance.firstCloth.headSprite);
        }
    }
    
    void ActiveSecondCloth()
    {
        _currSecondCloth = SaveManager.Instance.secondCloth;

        if(SaveManager.Instance.setup.secondClothType != ClothType.NONE_Second && playerUI.secondClothFill.canUse && playerUI.canChangeCloth) 
        {
            _currSecondCloth.cloth.ChangePWUP(SaveManager.Instance.secondCloth.tex, SaveManager.Instance.secondCloth.jetpackMat);
            playerUI.CheckFirstCloth(false);
            playerUI.ChangeMainCloth(SaveManager.Instance.secondCloth.headSprite);
        }
    }
}
