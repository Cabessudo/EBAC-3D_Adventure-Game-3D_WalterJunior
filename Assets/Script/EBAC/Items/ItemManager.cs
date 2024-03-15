using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;

namespace Items
{
    public enum ItemType
    {
        Coin,
        LifePack
    }

    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itemSetup;

        void Start()
        {
            Reset();
        }

        void Reset()
        {
            foreach(var itens in itemSetup)
            {
                itens.soInt.value = 0;
            }

            //itemSetup.ForEach(i => i.soInt.value = 0);
        }

        public ItemSetup GetItemByType(ItemType type)
        {
            var item = itemSetup.Find(i => i.itemType == type);
            return item;
        }

        public void AddItemByType(ItemType itemType, int amount = 1)
        {
            itemSetup.Find(i => i.itemType == itemType).soInt.value += amount;
        }

        public void RemoveItemByType(ItemType itemType, int amount = 1)
        {
            var item = itemSetup.Find(i => i.itemType == itemType);
            item.soInt.value -= amount;

            if(item.soInt.value <= 0) item.soInt.value = 0;
        }

        [NaughtyAttributes.Button]
        void Test()
        {
            RemoveItemByType(ItemType.Coin);
        }
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite sprite;
    }
}    