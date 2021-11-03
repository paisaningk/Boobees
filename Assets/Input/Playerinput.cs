//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.1.1
//     from Assets/Input/Playerinput.inputactions
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

public partial class @Playerinput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Playerinput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Playerinput"",
    ""maps"": [
        {
            ""name"": ""PlayerAction"",
            ""id"": ""b3495435-86eb-48c5-87db-9fb24cbc4a0e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""b2d32202-0f89-401a-856c-86544f1978ff"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""3b1cf35a-416a-4528-a4f8-8f0b182094f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""0291cf87-6558-4b01-a30f-b77163f34483"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""6969f10d-c931-4bfd-9b2e-f26dc382c236"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""01a8d12c-caf9-48ee-a8bf-9e3decf0835b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""43761efa-7c9c-4531-8d6b-9b251215ef19"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""key and mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a030f45f-f5f2-4c25-885a-f2b92e05ee3a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""key and mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""946eded0-4a75-40c1-8c1c-b0f0482326a9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""key and mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""834a9659-a72f-490c-915c-f8a21bda147e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""key and mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7a87511d-b024-4805-a2e7-fe86365c81d6"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eda4540d-f130-4743-be1b-a5a25fa97703"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""key and mouse"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0aac234-b7a9-4349-830a-7c7ca6035717"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""key and mouse"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70cdcfa8-73de-45c1-bfa9-ec8b9b106450"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""key and mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc234798-9c11-475d-b6c2-94e870b56927"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""key and mouse"",
            ""bindingGroup"": ""key and mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerAction
        m_PlayerAction = asset.FindActionMap("PlayerAction", throwIfNotFound: true);
        m_PlayerAction_Move = m_PlayerAction.FindAction("Move", throwIfNotFound: true);
        m_PlayerAction_Attack = m_PlayerAction.FindAction("Attack", throwIfNotFound: true);
        m_PlayerAction_Dash = m_PlayerAction.FindAction("Dash", throwIfNotFound: true);
        m_PlayerAction_Pause = m_PlayerAction.FindAction("Pause", throwIfNotFound: true);
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

    // PlayerAction
    private readonly InputActionMap m_PlayerAction;
    private IPlayerActionActions m_PlayerActionActionsCallbackInterface;
    private readonly InputAction m_PlayerAction_Move;
    private readonly InputAction m_PlayerAction_Attack;
    private readonly InputAction m_PlayerAction_Dash;
    private readonly InputAction m_PlayerAction_Pause;
    public struct PlayerActionActions
    {
        private @Playerinput m_Wrapper;
        public PlayerActionActions(@Playerinput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerAction_Move;
        public InputAction @Attack => m_Wrapper.m_PlayerAction_Attack;
        public InputAction @Dash => m_Wrapper.m_PlayerAction_Dash;
        public InputAction @Pause => m_Wrapper.m_PlayerAction_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PlayerAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionActions instance)
        {
            if (m_Wrapper.m_PlayerActionActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnMove;
                @Attack.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnAttack;
                @Dash.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnDash;
                @Pause.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerActionActions @PlayerAction => new PlayerActionActions(this);
    private int m_keyandmouseSchemeIndex = -1;
    public InputControlScheme keyandmouseScheme
    {
        get
        {
            if (m_keyandmouseSchemeIndex == -1) m_keyandmouseSchemeIndex = asset.FindControlSchemeIndex("key and mouse");
            return asset.controlSchemes[m_keyandmouseSchemeIndex];
        }
    }
    public interface IPlayerActionActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
