using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Audio;

public class ItemColletableBase : MonoBehaviour
{
    [Header("Item References")]
    public ItemType itemType;
    public MeshRenderer mesh;
    public Collider itemCollider;
    //Audio
    public SFXType sfxType;
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
        if(collectVfx != null) collectVfx.Play();
        if(mesh != null) mesh.enabled = false;
        if(itemCollider != null) itemCollider.enabled = false;
        SFXPool.Instance?.Play(sfxType);
        ItemManager.Instance?.AddItemByType(itemType);
        ItemLayoutManager.Instance.itemLayouts?.ForEach(i => i.UpadateUI());
        SFXManager.Instance.SetAudioByType(Audio.SFXType.ITEM_COIN, collectSfx);
        Destroy(gameObject, 1);
    }
}
