using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChangeScene : MonoBehaviour
{
    public Image curtainScene;
    public Color curtainDown;
    public Color curtainUp;
    private Ease ease = Ease.Linear;
    public float duration = .1f;

    void Start()
    {
        SetUpTheCurtain();
    }

    public void SetDownTheCurtain()
    {
        curtainScene.DOColor(curtainDown, duration).SetEase(ease);
    }

    public void SetUpTheCurtain()
    {
        curtainScene.DOColor(curtainUp, duration).SetEase(ease);
    }
}
