using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Texts;

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

    void OnTriggerEnter(Collider other)
    {
        if(!_chestOpenned && !locked && other.tag == "Player")
            ShowIcon();
    }

    void OnTriggerExit(Collider other)
    {
        if(!_chestOpenned && !locked && other.tag == "Player")
            HideIcon();
    }


    protected virtual void OpenChest()
    {
        PlayerUI.Instance?.PlayerUIAnim(Anim.AnimUIType.OPEN_CHEST);
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
        TextManagerUI.Instance.SetTextByType(TextType.CHEST);
    }

    void HideIcon()
    {
        warn.SetActive(false);
    }
    #endregion
}
