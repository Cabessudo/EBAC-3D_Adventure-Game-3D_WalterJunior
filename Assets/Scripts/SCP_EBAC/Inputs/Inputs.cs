//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Scripts/SCP_EBAC/Inputs/Inputs.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Inputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""7d8c00be-e4c1-49f3-8d64-66e74e3bd39a"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""1fb7f902-db43-4161-985b-49b6b7e86791"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Change To First Gun"",
                    ""type"": ""Button"",
                    ""id"": ""ec75a663-8954-4d5c-8f73-0ec1a98b04e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Change To Second Gun"",
                    ""type"": ""Button"",
                    ""id"": ""d6968fc3-97fe-46cc-b0ad-06465ea35078"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Change To First Cloth"",
                    ""type"": ""Button"",
                    ""id"": ""f4c5825d-a826-445b-aa54-df5135f98b52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Change To Second Cloth"",
                    ""type"": ""Button"",
                    ""id"": ""e761aab8-7d88-4d1e-b54e-81e75c151eaf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""419d8caa-d9ec-4ab1-aac6-9b2aa2cbc69d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1e8c24d-3b7a-4170-81a7-cf610f36d28a"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Change To First Gun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""814146de-91f3-4dbd-8c06-0db8cc795e4c"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Change To Second Gun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec3680af-cabd-4865-a40c-de52e2c54274"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Change To First Cloth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65b83a52-1343-4983-976f-51b19d1af3b5"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Change To Second Cloth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Shoot = m_Gameplay.FindAction("Shoot", throwIfNotFound: true);
        m_Gameplay_ChangeToFirstGun = m_Gameplay.FindAction("Change To First Gun", throwIfNotFound: true);
        m_Gameplay_ChangeToSecondGun = m_Gameplay.FindAction("Change To Second Gun", throwIfNotFound: true);
        m_Gameplay_ChangeToFirstCloth = m_Gameplay.FindAction("Change To First Cloth", throwIfNotFound: true);
        m_Gameplay_ChangeToSecondCloth = m_Gameplay.FindAction("Change To Second Cloth", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private List<IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<IGameplayActions>();
    private readonly InputAction m_Gameplay_Shoot;
    private readonly InputAction m_Gameplay_ChangeToFirstGun;
    private readonly InputAction m_Gameplay_ChangeToSecondGun;
    private readonly InputAction m_Gameplay_ChangeToFirstCloth;
    private readonly InputAction m_Gameplay_ChangeToSecondCloth;
    public struct GameplayActions
    {
        private @Inputs m_Wrapper;
        public GameplayActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot => m_Wrapper.m_Gameplay_Shoot;
        public InputAction @ChangeToFirstGun => m_Wrapper.m_Gameplay_ChangeToFirstGun;
        public InputAction @ChangeToSecondGun => m_Wrapper.m_Gameplay_ChangeToSecondGun;
        public InputAction @ChangeToFirstCloth => m_Wrapper.m_Gameplay_ChangeToFirstCloth;
        public InputAction @ChangeToSecondCloth => m_Wrapper.m_Gameplay_ChangeToSecondCloth;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void AddCallbacks(IGameplayActions instance)
        {
            if (instance == null || m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
            @Shoot.started += instance.OnShoot;
            @Shoot.performed += instance.OnShoot;
            @Shoot.canceled += instance.OnShoot;
            @ChangeToFirstGun.started += instance.OnChangeToFirstGun;
            @ChangeToFirstGun.performed += instance.OnChangeToFirstGun;
            @ChangeToFirstGun.canceled += instance.OnChangeToFirstGun;
            @ChangeToSecondGun.started += instance.OnChangeToSecondGun;
            @ChangeToSecondGun.performed += instance.OnChangeToSecondGun;
            @ChangeToSecondGun.canceled += instance.OnChangeToSecondGun;
            @ChangeToFirstCloth.started += instance.OnChangeToFirstCloth;
            @ChangeToFirstCloth.performed += instance.OnChangeToFirstCloth;
            @ChangeToFirstCloth.canceled += instance.OnChangeToFirstCloth;
            @ChangeToSecondCloth.started += instance.OnChangeToSecondCloth;
            @ChangeToSecondCloth.performed += instance.OnChangeToSecondCloth;
            @ChangeToSecondCloth.canceled += instance.OnChangeToSecondCloth;
        }

        private void UnregisterCallbacks(IGameplayActions instance)
        {
            @Shoot.started -= instance.OnShoot;
            @Shoot.performed -= instance.OnShoot;
            @Shoot.canceled -= instance.OnShoot;
            @ChangeToFirstGun.started -= instance.OnChangeToFirstGun;
            @ChangeToFirstGun.performed -= instance.OnChangeToFirstGun;
            @ChangeToFirstGun.canceled -= instance.OnChangeToFirstGun;
            @ChangeToSecondGun.started -= instance.OnChangeToSecondGun;
            @ChangeToSecondGun.performed -= instance.OnChangeToSecondGun;
            @ChangeToSecondGun.canceled -= instance.OnChangeToSecondGun;
            @ChangeToFirstCloth.started -= instance.OnChangeToFirstCloth;
            @ChangeToFirstCloth.performed -= instance.OnChangeToFirstCloth;
            @ChangeToFirstCloth.canceled -= instance.OnChangeToFirstCloth;
            @ChangeToSecondCloth.started -= instance.OnChangeToSecondCloth;
            @ChangeToSecondCloth.performed -= instance.OnChangeToSecondCloth;
            @ChangeToSecondCloth.canceled -= instance.OnChangeToSecondCloth;
        }

        public void RemoveCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameplayActions instance)
        {
            foreach (var item in m_Wrapper.m_GameplayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnShoot(InputAction.CallbackContext context);
        void OnChangeToFirstGun(InputAction.CallbackContext context);
        void OnChangeToSecondGun(InputAction.CallbackContext context);
        void OnChangeToFirstCloth(InputAction.CallbackContext context);
        void OnChangeToSecondCloth(InputAction.CallbackContext context);
    }
}
