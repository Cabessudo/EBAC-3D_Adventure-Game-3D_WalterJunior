using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAbilityBase : MonoBehaviour
{
    protected MyPlayer player;
    protected Inputs inputs;

    private void OnValidate()
    {
        if(player == null) player = GetComponent<MyPlayer>();
    }

    void Start()
    {
        inputs = new Inputs();
        inputs.Enable();

        Init();
        OnValidate();
        RegisterListeners();
    }

    private void OnEnable()
    {
        if(inputs != null)
            inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    protected virtual void Init() {}
    protected virtual void RegisterListeners() {}
    protected virtual void RemoveListeners() {}

}
