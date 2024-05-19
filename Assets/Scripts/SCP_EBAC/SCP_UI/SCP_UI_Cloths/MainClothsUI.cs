using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainClothsUI : ClothsUIBase
{
    public Image clothImage;
    public Sprite spriteDefault;
    public bool selected;
    public bool firstCloth;

    void Start()
    {
        Invoke(nameof(Init), .2f);
    }

    void Init()
    {
        GetItemCloth();
        CheckCloth();
    }

    public void GetItemCloth()
    {
        if(firstCloth)
            GetClothSprite(SaveManager.Instance.firstCloth?.clothSprite);
        else
            GetClothSprite(SaveManager.Instance.secondCloth?.clothSprite);
    } 

    void CheckCloth()
    {
        if(clothImage.sprite == null)
            clothImage.sprite = spriteDefault;
    }

    public void SelectMainCloth()
    {
        selected = true;
        transform.localScale *= 1.1f;
    }
    
    public void GetClothSprite(Sprite s)
    {
        clothImage.sprite = s;
    }
}
