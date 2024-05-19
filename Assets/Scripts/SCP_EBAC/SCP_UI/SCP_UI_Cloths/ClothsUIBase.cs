using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ClothsUIBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected Vector3 defaultScale = Vector3.one;


    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        ClothsUIManager.Instance.UnselectAllCloths();
        ClothsUIManager.Instance.SelectCloth(this.transform);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = defaultScale;
    }

    
}
