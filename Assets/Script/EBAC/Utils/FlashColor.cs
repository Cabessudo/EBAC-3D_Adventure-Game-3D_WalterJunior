using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    private Color _damageColor = Color.red;


    [Header("Animation")]
    private Tween _currTween;
    public float duration = .1f;

    void OnValidate()
    {
        if(meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        if(skinnedMeshRenderer == null) skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    public void Flash()
    {
        if(skinnedMeshRenderer != null && !_currTween.IsActive())
            _currTween = skinnedMeshRenderer.material.DOColor(_damageColor, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);

        if(meshRenderer != null && !_currTween.IsActive())
            _currTween = meshRenderer.material.DOColor(_damageColor, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
    }

    public void KillTween()
    {
        if(_currTween != null)
            _currTween.Kill();
    }
}
