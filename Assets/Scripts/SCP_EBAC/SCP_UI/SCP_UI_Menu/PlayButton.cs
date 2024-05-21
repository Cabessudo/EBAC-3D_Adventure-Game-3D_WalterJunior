using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayButton : MonoBehaviour
{
    public Transform rocket;
    public List<Transform> planetFloorUI;
    public float planetsY;
    private bool show = false;
    public bool interact;

    [Header("Animation")]
    public GameObject rocketFlameAnim;
    public Ease ease;
    public float moveDuration = 1;
    public float rockectduration;


    public void ShowandHidePlanets()
    {
        if(!MenuClothUIManager.Instance.clothOpen)
        {
            if(!show && !interact) //Show Planets
            {
                interact = true;
                planetFloorUI.ForEach(i => i.DOKill());
                planetFloorUI.ForEach(i => i.DOMoveY(planetsY, moveDuration).SetEase(ease).SetRelative().OnComplete(
                    delegate
                    { 
                        interact = false;
                        rocketFlameAnim.SetActive(false);
                    }));

                Rocket();
                show = true;
                rocketFlameAnim.SetActive(true);
            }
            else if(show && !interact) //Hide Planets
            {
                interact = true;
                planetFloorUI.ForEach(i => i.DOKill());
                Rocket();
                planetFloorUI.ForEach(i => i.DOMoveY(-planetsY, moveDuration).SetEase(ease).SetRelative().OnComplete(
                    delegate{ interact = false;}));
                show = false;
            }
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
        planetFloorUI.ForEach(i => i.DOKill());
    }
}
