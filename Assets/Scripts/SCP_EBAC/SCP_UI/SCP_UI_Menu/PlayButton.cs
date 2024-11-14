using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayButton : MonoBehaviour
{
    public Animator anim;
    public Transform rocket;
    public Transform planet;
    private bool show = false;
    public bool canInteract = true;

    [Header("Animation")]
    public GameObject rocketFlameAnim;
    public Ease ease;
    public float moveDuration = 1;
    public float rockectSpinDuration;
    public float waitAnimDuration;

    void Awake()
    {
        canInteract = true;
    }

    void Rocket()
    {
        if(!show) //When planets showed, the rocket start spin
        {
            rocket.DOKill();
            rocket.DORotateQuaternion(Quaternion.Euler(0,0,-45), rockectSpinDuration).SetDelay(moveDuration).SetEase(ease);
        }
        else //Back to its start rotation
        {
            rocket.DOKill();
            rocket.DORotateQuaternion(Quaternion.Euler(0,0,0), moveDuration).SetEase(ease);
        }
    }

    void OnDestroy()
    {
        rocket.DOKill();
        planet.DOKill();
    }

    public void ShowAndHidePlanetsAnim()
    {
        if(!MenuClothUIManager.Instance.clothOpen)
        {
            if(!show && canInteract)
            {
                canInteract = false;
                Invoke(nameof(WaitAnim), waitAnimDuration);
                Rocket();
                rocketFlameAnim.SetActive(true);
                anim.SetTrigger("Show");
                show = true;
            }
            else if(show && canInteract) 
            {
                canInteract = false;
                Invoke(nameof(WaitAnim), waitAnimDuration);
                Rocket(); 
                anim.SetTrigger("Hide");
                show = false;
            }
        }
    }

    //Wait animation to show or hide planets, to not occur a bug
    void WaitAnim()
    {
        canInteract = true;
    }
}
