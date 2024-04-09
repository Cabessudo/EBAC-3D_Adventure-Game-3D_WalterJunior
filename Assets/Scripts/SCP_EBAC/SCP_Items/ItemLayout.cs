using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Items
{
    public class ItemLayout : MonoBehaviour
    {
        private ItemSetup _currSetup;

        public Image itemImage;
        public TextMeshProUGUI itemAmount;

        public void GetItemSetup(ItemSetup setup)
        {
            _currSetup = setup;
        }
        
        public void UpadateUI()
        {
            itemImage.sprite = _currSetup.sprite;
            itemAmount.SetText(_currSetup.soInt.value.ToString());
        }
    }
}
