using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Items;

namespace Items
{
    public class ItemLayout : MonoBehaviour
    {
        private ItemSetup _currSetup;
        public Color coinColor;
        public Color lifePackColor;

        public Image itemImage;
        public TextMeshProUGUI itemAmount;

        public void GetItemSetup(ItemSetup setup)
        {
            _currSetup = setup;

            if(setup.itemType == ItemType.Coin)
                itemAmount.color = coinColor;
                
            if(setup.itemType == ItemType.LifePack)
                itemAmount.color = lifePackColor;

        }
        
        public void UpadateUI()
        {
            itemImage.sprite = _currSetup.sprite;
            itemAmount.SetText("x" + _currSetup.soInt.value);
        }
    }
}
