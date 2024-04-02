using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 _defaultScale;
    public Ease ease = Ease.Linear;
    public float scale;
    public float duration = 1;

    void Start()
    {
        _defaultScale = gameObject.transform.localScale;    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
        transform.localScale = _defaultScale;
        transform.DOScale(_defaultScale * scale, duration).SetEase(ease);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(_defaultScale, duration).SetEase(ease);
    }
}
