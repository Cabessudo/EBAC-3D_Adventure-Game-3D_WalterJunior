using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBase : MonoBehaviour
{
    public Animator anim;
    public GameObject warn;
    public KeyCode key = KeyCode.O;
    public string triggerOpen = "Open";

    public ChestItemCoin coins;
    private bool _chestOpenned;
    public bool locked = false;

    void Start()
    {
        HideIcon();
    }

    void Update()
    {
        if(Input.GetKeyDown(key) && !_chestOpenned && warn.activeSelf && !locked)
            OpenChest(); 
    }

    void OnTriggerEnter()
    {
        if(!_chestOpenned && !locked)
            ShowIcon();
    }

    void OnTriggerExit()
    {
        if(!_chestOpenned && !locked)
            HideIcon();
    }


    protected virtual void OpenChest()
    {
        anim.SetTrigger(triggerOpen);
        _chestOpenned = true;
        Invoke(nameof(ShowItems), .5f);
    }

    #region Item
    private void ShowItems()
    {
        coins.ShowItem();
        Invoke(nameof(Collect), 1);
    }

    private void Collect()
    {
        coins.Collect();
    }

    #endregion

    #region  Iteraction
    void ShowIcon()
    {
        warn.SetActive(true);
    }

    void HideIcon()
    {
        warn.SetActive(false);
    }
    #endregion
}
