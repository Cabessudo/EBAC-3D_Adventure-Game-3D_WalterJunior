using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIGunUpdater : MonoBehaviour
{
    public Image gunImage;

    [Header("Anim")]
    private Tween _currTween;
    private Ease ease = Ease.Linear;
    public float duration = .1f;

    void OnValidate()
    {
        if(gunImage == null) gunImage = GetComponent<Image>();        
    }

    public void UpdateValue(float f)
    {
        gunImage.fillAmount = f;
    }

    public void UpdateValue(float curr, float max)
    {
        // gunImage.fillAmount = 1 - (curr/max);
        if(_currTween != null) _currTween.Kill();
        gunImage.DOFillAmount(1 - (curr/max), duration).SetEase(ease);
    }
}
