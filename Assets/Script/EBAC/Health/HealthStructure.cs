using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStructure : HealthBase
{
    public MeshRenderer meshRenderer;

    protected override void Kill()
    {
        if(destroyOnDeath)
            meshRenderer.enabled = false;

        onKill?.Invoke();
    }
}
