using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;

public class MenuClothUIManager : Singleton<MenuClothUIManager>
{
    [Header("Cloths")]
    public List<GameObject> cloths;
    public bool clothOpen;


    void Start()
    {
        Init();
    }

    void Init()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ShowAndHideCloths(false);
    }

    public void ShowAndHideCloths(bool b)
    {
        if(b)
        {
            cloths.ForEach(i => i.SetActive(true));
            clothOpen = b;
        }
        else
        {
            cloths.ForEach(i => i.SetActive(false));
            clothOpen = b;
        }
    }
}
