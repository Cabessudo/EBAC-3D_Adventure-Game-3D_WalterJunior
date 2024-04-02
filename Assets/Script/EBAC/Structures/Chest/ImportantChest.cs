using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;
using DG.Tweening;

public class ImportantChest : ChestBase
{
    public ParticleSystem particles;
    public ClothItemBase cloth;
    public Transform container;

    [Header("Spawn Anim")]
    private Ease ease = Ease.Linear;
    public float y = 5;
    public float duration = 1;
    public float delay = 1;

    protected override void OpenChest()
    {
        base.OpenChest();
        SpawnItem();
    }

    void SpawnItem()
    {
        var item = Instantiate(cloth, transform);
        item.transform.DOScale(0, duration).SetDelay(delay).SetEase(ease).From();
        item.transform.DOMoveY(y, duration).SetDelay(delay).SetEase(ease).SetRelative();
        Invoke(nameof(CanCollect), 2);
    }

    public void Unlocked()
    {
        if(locked)
        {
            locked = false;
            particles.Play();
        }
    }

    void CanCollect()
    {
        cloth.canCollect = true;
    }
}
