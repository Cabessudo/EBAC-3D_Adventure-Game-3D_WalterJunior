using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ebac.Singleton;
using DG.Tweening;
using Anim;

public class PlayerUI : Singleton<PlayerUI>
{
    [Header("Cloths")]
    public Image mainCloth;
    public List<Image> firstCloth;
    public List<Image> secondCloth;
    public bool canChangeCloth = true;
    

    [Header("ClothsFill")]
    public UIFillClothUpdater firstClothFill;
    public UIFillClothUpdater secondClothFill;

    [Header("Cloths Background")]
    public Image firstClothBackground;
    public Image secondClothBackground;
    public bool first;
    private Vector3 vector = new Vector3(.9f,.9f,.9f);

    [Header("Anim")]
    public AnimationUI anim;
    
    void Start()
    {
        Invoke(nameof(Init), .1f);
    }

    void Init()
    {
        GetFirstCloth();
        GetSecondCloth();
        CheckCloth();
        canChangeCloth = true;
    }

    public void ChangeMainCloth(Sprite s)
    {
        mainCloth.sprite = s;

        if(first)
        {
            firstClothFill.image.fillAmount = 0;
            firstClothFill.transform.localScale = vector;
            firstClothFill.canUse = false;
            canChangeCloth = false;
        }
        else
        {
            secondClothFill.image.fillAmount = 0;
            secondClothFill.transform.localScale = vector;
            secondClothFill.canUse = false;
            canChangeCloth = false;
        }
    }

    public void GetFirstCloth()
    {
        firstCloth.ForEach(i => i.sprite = SaveManager.Instance.firstCloth.clothSprite);
        if(firstClothBackground.transform.localScale == Vector3.zero)
        {
            firstClothBackground.transform.DOScale(1, .1f).SetEase(Ease.Linear);
            firstClothFill.image.fillAmount = 0;
            firstClothFill.transform.localScale = vector;
        }
    }

    public void GetSecondCloth()
    {
        secondCloth.ForEach(i => i.sprite = SaveManager.Instance.secondCloth.clothSprite);
        if(secondClothBackground.transform.localScale == Vector3.zero)
        {
            secondClothBackground.transform.DOScale(1, .1f).SetEase(Ease.Linear);
            secondClothFill.image.fillAmount = 0;
            secondClothFill.transform.localScale = vector;
        }
    }

    public void CheckFirstCloth(bool b)
    {
        first = b;
    }

    public void CheckCloth()
    {
        if(SaveManager.Instance.setup.firstClothType == Cloth.ClothType.NONE_First)
            firstClothBackground.transform.localScale = Vector3.zero;

        if(SaveManager.Instance.setup.secondClothType == Cloth.ClothType.NONE_Second)
            secondClothBackground.transform.localScale = Vector3.zero;
    }

    public void PlayerUIAnim(AnimUIType currType)
    {
        anim?.SetAnimByType(currType);
    }
}
