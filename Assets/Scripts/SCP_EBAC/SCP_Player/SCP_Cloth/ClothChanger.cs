using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

public class ClothChanger : MonoBehaviour
{
    public List<SkinnedMeshRenderer> mesh;
    public Texture tex;
    public string shaderIdName = "_EmissionMap";

    private Texture _defaultTex;

    void Awake()
    {
        _defaultTex = mesh[0].material.GetTexture(shaderIdName);
    }

    void Start()
    {
        Invoke(nameof(Test), .5f);
    }

    void Test()
    {
        if(SaveManager.Instance.setup.playerCloth != null)
            ChangeCloth(SaveManager.Instance.setup.playerCloth);
    }

    public void ChangeCloth(Texture tex)
    {
        foreach(var m in mesh)
        {
            m.material.SetTexture(shaderIdName, tex);
        }
    }

    public void ResetCloth()
    {
        foreach(var m in mesh)
        {
            m.material.SetTexture(shaderIdName, _defaultTex);
        }
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
