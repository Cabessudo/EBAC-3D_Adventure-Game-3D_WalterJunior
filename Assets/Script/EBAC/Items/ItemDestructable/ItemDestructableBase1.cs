using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemDestructableBase1 : MonoBehaviour
{
    public HealthBase itemHealth;
    private Coroutine _currRoutine;

    [Header("Item Drop")]
    public GameObject itemToDrop;
    public Transform dropPos;
    public int amountDrop = 5;
    public float timeBtwDrop = .25f;
    private bool once;

    [Header("Damage Anim")]
    public float duration = 1;
    public int shakeForce = 3;

    void OnValidate()
    {
        if(itemHealth == null) itemHealth = GetComponent<HealthBase>();
    }

    void Awake()
    {
        OnValidate();
    }

    void Start()
    {
        itemHealth.onDamage += OnDamage;
        itemHealth.onKill += OnTreeDestroy;
    }

    void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(duration, Vector3.up/5, shakeForce);
    }
    
    void OnTreeDestroy()
    {
        if(!once)
        {
            once = true;
            StartCoroutine(DropItem());
            Invoke(nameof(RemoveHealthActions), timeBtwDrop * amountDrop);
        }
    }

    void RemoveHealthActions()
    {
        itemHealth.onDamage -= OnDamage;
        itemHealth.onKill -= OnTreeDestroy;
        Destroy(gameObject, .1f);
    }

    IEnumerator DropItem()
    {
        for(int i = 0; i < amountDrop; i++)
        {
            var item = Instantiate(itemToDrop, dropPos.position, dropPos.rotation);
            item.transform.DOScale(0, duration).SetEase(Ease.OutBack).From();
            yield return new WaitForSeconds(timeBtwDrop);
        }
    }
}
