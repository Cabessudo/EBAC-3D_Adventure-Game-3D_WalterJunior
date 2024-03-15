using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Items
{
    public class ItemLayoutManager : MonoBehaviour
    {
        public ItemLayout prefabLayout;
        public Transform container;

        public List<ItemLayout> itenLayouts;

        void Start()
        {
            CreateItem();
        }

        void CreateItem()
        {
            foreach(var items in ItemManager.Instance.itemSetup)
            {
                var item = Instantiate(prefabLayout, container);
                item.Load(items);
                itenLayouts.Add(item);
            }
        }
    }
}
