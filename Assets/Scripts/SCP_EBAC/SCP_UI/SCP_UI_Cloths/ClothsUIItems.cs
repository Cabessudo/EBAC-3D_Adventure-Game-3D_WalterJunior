using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Cloth;

public class ClothsUIItems : ClothsUIBase, IPointerEnterHandler, IPointerExitHandler
{
    public ClothType clothType;
    private  ClothSetup _currSetup;
    public GameObject imageLocked;

    //already selected
    public bool already;
    //Cloth equal to first or second
    public bool equal;

    public bool unlocked;

    void Start()
    {
        _currSetup = ClothManager.Instance.GetClothByType(clothType);
        
        Init();
    }

    void Update()
    {
        equal = ClothEqual();
        already = ClothAlreadySelected();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if(ClothsUIManager.Instance.firstCloth || ClothsUIManager.Instance.secondCloth)
        {
            base.OnPointerEnter(eventData);
        }
    }

    void Init()
    {
        var itemUnlocked = SaveManager.Instance.setup.clothsUnlockeds.Find(i => i == clothType);
        if(itemUnlocked == clothType)
        {
            imageLocked.SetActive(false);
            unlocked = true;
        }
    }

    [NaughtyAttributes.Button]
    public void SelectCloth()
    {
        if(ClothEqual() && !ClothAlreadySelected() && unlocked)
        {
            if(ClothsUIManager.Instance.firstCloth)
            {
                SaveManager.Instance.SaveFirstCloth(_currSetup);
            }
            
            
            if(ClothsUIManager.Instance.secondCloth)
                SaveManager.Instance.SaveSecondCloth(_currSetup);

            ClothsUIManager.Instance.SetClothSprite(_currSetup.clothSprite);
            Debug.Log(".");
        }

    }

    bool ClothEqual()
    {
        if(SaveManager.Instance.firstCloth.type != SaveManager.Instance.secondCloth.type)
            return true;
        else
            return false;
    }

    bool ClothAlreadySelected()
    {
        if(clothType == SaveManager.Instance.firstCloth.type)
            return true;
        else if(clothType == SaveManager.Instance.secondCloth.type)
            return true;
        else
            return false;
    }

    // Previous Selected
    // bool SelectedCheck()
    // {
    //     if(SaveManager.Instance.firstCloth != null && clothType == SaveManager.Instance.firstCloth.type)
    //         return true;
    //     else if(SaveManager.Instance.secondCloth != null && clothType == SaveManager.Instance.secondCloth.type)
    //         return true;
    //     else
    //     return false;
    // }
}
