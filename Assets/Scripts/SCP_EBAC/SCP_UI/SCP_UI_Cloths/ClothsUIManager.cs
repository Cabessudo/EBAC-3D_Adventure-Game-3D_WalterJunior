using System.Collections;
using System.Collections.Generic;
using Ebac.Singleton;
using UnityEngine;
using DG.Tweening;

public class ClothsUIManager : Singleton<ClothsUIManager>
{
    public List<ClothsUIItems> itemCloths;
    public List<MainClothsUI> clothsToSelect;
    private Vector3 defaultScale;
    public MainClothsUI firstClothUI;
    public bool firstCloth;
    public MainClothsUI secondClothUI;
    public bool secondCloth;

    void Start()
    {
        defaultScale = Vector3.one;
    }

    public void SelectCloth(Transform t)
    {
        t.DOScale(defaultScale * 1.1f, .1f).SetEase(Ease.Linear);
    }
    
    public void UnselectAllCloths()
    {
        itemCloths.ForEach(i => i.transform.localScale = defaultScale);
        clothsToSelect.ForEach(i => i.transform.localScale = defaultScale);
        // ResetIndex();
    }

    public void SecondClothButton()
    {
        secondCloth = true;
        firstCloth = false;
        // ResetIndex();
        // up = false;
    }

    public void FirstClothButton()
    {
        firstCloth = true;
        secondCloth = false;
        // ResetIndex();
        // up = false;
    }

    public void SetClothSprite(Sprite s)
    {
        if(firstCloth)
            firstClothUI.GetClothSprite(s);
        
        if(secondCloth)
            secondClothUI.GetClothSprite(s);
    }
}

/*

//Select The Cloths By Keys
    
    public bool up = true;
    private bool onBoard;
    public int _index = 0;

    void Start()
    {
        ResetIndex();
        SelectCloth(clothsToSelect[_index].transform);
    }
    
    void Update()
    {

        Region  Right
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(up)
            {
                _index++;
                UnselectCloth(clothsToSelect[_index - 1].transform);

                if(_index >= clothsToSelect.Count)
                {
                    _index = 0;
                    UnselectCloth(clothsToSelect[clothsToSelect.Count -1].transform);
                }

                SelectCloth(clothsToSelect[_index].transform);
            }
            else //up false
            {
                if(secondCloth || firstCloth )
                {
                    _index++;
                    UnselectCloth(itemCloths[_index - 1].transform);

                    if(_index >= itemCloths.Count)
                    {
                        _index = 0;
                        UnselectCloth(itemCloths[itemCloths.Count -1].transform);
                    }

                    SelectCloth(itemCloths[_index].transform);
                }

            }
        }
        #endregion

        Region Left
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(up)
            {
                _index--;
                UnselectCloth(clothsToSelect[_index + 1].transform);

                if(_index < 0)
                {
                    _index = clothsToSelect.Count - 1;
                    UnselectCloth(clothsToSelect[0].transform);
                }
                else if(up)
                {
                    _index = 0;
                }

                SelectCloth(clothsToSelect[_index].transform);
            }
            else //up false
            {
                if(secondCloth || firstCloth)
                {
                    _index--;
                    UnselectCloth(itemCloths[_index + 1].transform);

                    if(_index < 0)
                    {
                        _index = itemCloths.Count - 1;
                        UnselectCloth(itemCloths[0].transform);
                    }

                    SelectCloth(itemCloths[_index].transform);
                }
            }
        }
        #endregion

        Region Select Main or Item Cloth
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) && onBoard)
        {
            if(!up && firstCloth)
                itemCloths[_index].SelectCloth();    
            else if(!up && secondCloth) 
                itemCloths[_index].SelectCloth();    
            
            if(up)
            {
                if(_index == 0)
                {
                    FirstClothButton();
                }
                else if(_index == 1)
                {
                    SecondClothButton();
                }

                SelectCloth(itemCloths[_index].transform);
            }
        }
        #endregion

        Region Change Main to Item Cloth or Vice Versa
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && !up)
        {
            UnselectAllCloths();
            onBoard = true;
            _index = 0;
            SelectCloth(clothsToSelect[_index].transform);
            up = true;
        }
        
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && up)
        {
            if(firstCloth || secondCloth)
            {
                UnselectAllCloths();
                onBoard = true;
                _index = 0;
                SelectCloth(itemCloths[_index].transform);
                up = false;

                if(firstCloth)
                    firstClothUI.SelectMainCloth();
                else if(secondCloth)
                    secondClothUI.SelectMainCloth();
            }
        }
        #endregion  
    }

    void UnselectCloth(Transform t)
    {
        t.transform.DOKill();
        t.transform.localScale = defaultScale;
        onBoard = true;
    }

    void ResetIndex()
    {
        _index = 0;
    }
*/