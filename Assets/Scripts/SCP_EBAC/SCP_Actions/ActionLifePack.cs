using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ActionLifePack : MonoBehaviour
{
    public SOInt soInt;
    public KeyCode key = KeyCode.L;

    void Start()
    {
        soInt = ItemManager.Instance.GetItemByType(ItemType.LifePack).soInt;
    }    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            RecoverLife();
        }
    }
    
    void RecoverLife()
    {
        if(soInt.value > 0)
        {
            MyPlayer.Instance.health.RestartLife();
            ItemManager.Instance.RemoveItemByType(ItemType.LifePack);
        }
    }
}
