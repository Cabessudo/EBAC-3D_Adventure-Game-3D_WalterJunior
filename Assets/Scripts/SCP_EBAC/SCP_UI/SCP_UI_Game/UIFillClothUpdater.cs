using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIFillClothUpdater : UIFillUpdater
{
    public bool canUse = true;

    void Start()
    {
        canUse = true;
    }

    public void SetDuration()
    {
        UpdateValueFill(duration);
        StartCoroutine(CanUseRoutine());
    }

    public IEnumerator CanUseRoutine()
    {
        yield return new WaitForSeconds(duration);
        canUse = true;
    }
}
