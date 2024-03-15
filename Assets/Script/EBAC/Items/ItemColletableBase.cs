using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ItemColletableBase : MonoBehaviour
{
    [Header("Item References")]
    public ItemType itemType;
    public MeshRenderer mesh;
    public Collider itemCollider;
    //Audio
    public AudioSource collectSfx;
    //VFX
    public ParticleSystem collectVfx;


    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Collect()
    {
        OnCollect();
    }

    void OnCollect()
    {
        if(collectSfx != null) collectSfx.Play();
        if(collectVfx != null) collectVfx.Play();
        if(mesh != null) mesh.enabled = false;
        if(itemCollider != null) itemCollider.enabled = false;
        ItemManager.Instance.AddItemByType(itemType);
        Destroy(gameObject, 1);
    }
}
