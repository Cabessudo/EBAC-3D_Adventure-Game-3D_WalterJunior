using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cloth;

public class ClothChanger : MonoBehaviour
{
    public List<SkinnedMeshRenderer> mesh;
    public Texture tex;
    public string shaderIdName = "_EmissionMap";

    private Texture _defaultTex;

    [Header("Jetpack")]
    public MeshRenderer jetpackRenderer;
    public Material mainJetpackMaterial;

    [Header("UI")]
    public Image playerHead;
    public Sprite mainCloth;

    void Awake()
    {
        _defaultTex = mesh[0].material.GetTexture(shaderIdName);
    }

    public void ChangeCloth(Texture currClothTex, Material currJetpackMaterial)
    {
        foreach(var m in mesh)
        {
            m.material.SetTexture(shaderIdName, currClothTex);
        }

        jetpackRenderer.material = currJetpackMaterial;
    }

    public void ResetCloth()
    {
        foreach(var m in mesh)
        {
            m.material.SetTexture(shaderIdName, _defaultTex);
        }

        playerHead.sprite = mainCloth;
        jetpackRenderer.material = mainJetpackMaterial;
    }
    
    [NaughtyAttributes.Button]
    void ChangeClothTest()
    {
        foreach(var m in mesh)
        {
            m.material.SetTexture(shaderIdName, tex);
        }
    }
}
