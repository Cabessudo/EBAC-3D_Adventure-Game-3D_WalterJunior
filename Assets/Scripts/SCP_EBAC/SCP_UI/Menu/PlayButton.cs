using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayButton : MonoBehaviour
{
    public Transform rocket;
    public Transform ui;
    public float planetsY;
    private bool show = false;
    private bool interact;

    [Header("Animation")]
    public Ease ease;
    public float moveDuration = 1;

    public float rockectduration;


    public void ShowandHidePlanets()
    {
        if(!show && !interact) //Show Planets
        {
            interact = true;
            ui.DOKill();
            ui.DOMoveY(planetsY, moveDuration).SetEase(ease).SetRelative().OnComplete(
                delegate{ interact = false;});
            Rocket();
            show = true;
        }
        else if(show && !interact) //Hide Planets
        {
            ui.DOKill();
            Rocket();
            ui.DOMoveY(-planetsY, moveDuration).SetEase(ease).SetRelative().OnComplete(
                delegate{ interact = false;});
            show = false;
        }
    }

    void Rocket()
    {
        if(!show) //GO
        {
            rocket.DOKill();
            rocket.DORotateQuaternion(Quaternion.Euler(0,0,-45), rockectduration).SetDelay(moveDuration).SetEase(ease);
        }
        else //BACK
        {
            rocket.DOKill();
            rocket.DORotateQuaternion(Quaternion.Euler(0,0,0), moveDuration).SetEase(ease);
        }
    }

    void OnDestroy()
    {
        rocket.DOKill();
        ui.DOKill();
    }
}
