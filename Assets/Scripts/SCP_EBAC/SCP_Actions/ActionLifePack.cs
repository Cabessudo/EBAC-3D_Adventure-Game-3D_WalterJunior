using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ActionLifePack : MonoBehaviour
{
    public int lifepackAmount;
    public KeyCode key = KeyCode.L;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            RecoverLife();
        }

        if(lifepackAmount != ItemManager.Instance.GetItemByType(ItemType.LifePack).itemAmount)
        {
            lifepackAmount = ItemManager.Instance.GetItemByType(ItemType.LifePack).itemAmount;
        }
    }
    
    void RecoverLife()
    {
        if(lifepackAmount > 0)
        {
            MyPlayer.Instance.health.RestartLife();
            ItemManager.Instance.RemoveItemByType(ItemType.LifePack, 1);
        }
    }
}
