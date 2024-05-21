using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;
using DG.Tweening;

public class ImportantChest : ChestBase
{
    public ParticleSystem particles;
    public ImportantItem importantItem;
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

    [NaughtyAttributes.Button]
    void SpawnItem()
    {
        var item = Instantiate(importantItem, container);
        item.transform.localPosition = Vector3.zero;
        item.transform.DOScale(0, duration).SetDelay(delay).SetEase(ease).From();
        item.transform.DOMoveY(y, duration).SetDelay(delay).SetEase(ease).SetRelative();
        StartCoroutine(CanCollect(item));
    }

    public void Unlocked()
    {
        if(locked)
        {
            locked = false;
            particles.Play();
        }
    }

    IEnumerator CanCollect(ImportantItem item)
    {
        yield return new WaitForSeconds(duration + delay);
        item.canCollect = true;
    }
}
