using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Items;

public class ChestItemCoin : ChestItemBase
{
    private List<GameObject> coins = new List<GameObject>();
    public GameObject coinObj;
    public int amountCoins = 5;
    public float pos = .1f;

    public float animDuration = 1;

    [NaughtyAttributes.Button]
    public override void ShowItem()
    {
        base.ShowItem();
        for(int i = 0; i < amountCoins; i++)
        {
            var item = Instantiate(coinObj);
            item.transform.position = transform.position + Vector3.forward * Random.Range(-pos,pos) + Vector3.right * Random.Range(-pos,pos) ;
            item.transform.DOScale(0, 1f).From();
            coins.Add(item);
        }
    }

    [NaughtyAttributes.Button]
    public override void Collect()
    {
        base.Collect();
        foreach(var coin in coins)
        {
            coin.transform.DOScale(0, animDuration/2);
            coin.transform.DOMoveY(1f, animDuration).SetRelative();
            Destroy(coin, animDuration*2);
            Invoke("Clean", animDuration*2);
            ItemManager.Instance.AddItemByType(ItemType.Coin, amountCoins);
        }
    }

    void Clean()
    {
        coins.Clear();
    }
}
