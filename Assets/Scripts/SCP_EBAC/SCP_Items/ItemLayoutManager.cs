using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;


namespace Items
{
    public class ItemLayoutManager : Singleton<ItemLayoutManager>
    {
        public ItemLayout prefabLayout;
        public Transform container;

        public List<ItemLayout> itemLayouts;

        void Start()
        {
            CreateItem();
            // itemLayouts.ForEach(i => i.Invoke(nameof(i.UpdateUI), .1f));
        }

        void CreateItem()
        {
            foreach(var items in ItemManager.Instance.itemSetup)
            {
                var item = Instantiate(prefabLayout, container);
                item.GetItemSetup(items);
                itemLayouts.Add(item);
            }
        }
    }
}
