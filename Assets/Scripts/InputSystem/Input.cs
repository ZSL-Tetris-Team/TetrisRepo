//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/InputSystem/Input.inputactions
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

public partial class @Input : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""Blocks"",
            ""id"": ""a0ba25c0-e34a-4c80-bac7-914982feed84"",
            ""actions"": [
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""561a374d-dd29-429a-bed1-f791dfe9710f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""d693da7f-9a0d-4651-94da-8895947dd4f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateLeft"",
                    ""type"": ""Button"",
                    ""id"": ""6ce3e3ae-cbad-4661-96be-225d8e10c861"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateRight"",
                    ""type"": ""Button"",
                    ""id"": ""d8e5619e-ec31-4ecf-90b1-2163ef75b67a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SoftDropHold"",
                    ""type"": ""Value"",
                    ""id"": ""dcf89304-8db1-48f9-bfc1-d5a5aedfdbc7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""HardDrop"",
                    ""type"": ""Button"",
                    ""id"": ""5193b32f-a91f-472f-9d87-b3d37fed2edc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HoldPiece"",
                    ""type"": ""Button"",
                    ""id"": ""2961c7a3-714d-4fda-905f-8b4b90482393"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SoftDropUp"",
                    ""type"": ""Button"",
                    ""id"": ""1cdd261b-e78e-41a1-a485-9967b03304e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SoftDropDown"",
                    ""type"": ""Button"",
                    ""id"": ""9c09aabc-157e-4e3c-9c52-c804b32a3f60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f9f4654c-907e-463b-9acc-2a980016462c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Blocks"",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2daf0ec1-e9f3-410b-b7e2-c1cb42b8d155"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Blocks"",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f72c1bf-0c28-401d-951b-13dd59f700e4"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Blocks"",
                    ""action"": ""RotateLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49b82eba-ff99-4df0-9d61-fda4ef207167"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Blocks"",
                    ""action"": ""RotateRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3f7a106-2582-4097-aa5c-385e73a2f348"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": ""Hold(duration=1.401298E-45)"",
                    ""processors"": """",
                    ""groups"": ""Blocks"",
                    ""action"": ""SoftDropHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e29512a-c34a-493e-81b2-010da6f1e7ae"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Blocks"",
                    ""action"": ""HardDrop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ad0921a-ff0e-4342-a849-8c61e323fb49"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Blocks"",
                    ""action"": ""HoldPiece"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""decd79cb-3b7b-46b8-af0d-179c250f9470"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Blocks"",
                    ""action"": ""SoftDropUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5681efad-cb50-4c7e-87b7-6e17f5af0b17"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Blocks"",
                    ""action"": ""SoftDropDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Ui"",
            ""id"": ""724e010a-c6a3-46ee-9b98-746c24e7690a"",
            ""actions"": [
                {
                    ""name"": ""TryAgain"",
                    ""type"": ""Button"",
                    ""id"": ""f202e53a-e7c5-4e17-8864-79e76a2dd607"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fd273084-b678-4cbd-a7fc-a9c2ea8c55ba"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Blocks"",
                    ""action"": ""TryAgain"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Blocks"",
            ""bindingGroup"": ""Blocks"",
            ""devices"": []
        }
    ]
}");
        // Blocks
        m_Blocks = asset.FindActionMap("Blocks", throwIfNotFound: true);
        m_Blocks_Right = m_Blocks.FindAction("Right", throwIfNotFound: true);
        m_Blocks_Left = m_Blocks.FindAction("Left", throwIfNotFound: true);
        m_Blocks_RotateLeft = m_Blocks.FindAction("RotateLeft", throwIfNotFound: true);
        m_Blocks_RotateRight = m_Blocks.FindAction("RotateRight", throwIfNotFound: true);
        m_Blocks_SoftDropHold = m_Blocks.FindAction("SoftDropHold", throwIfNotFound: true);
        m_Blocks_HardDrop = m_Blocks.FindAction("HardDrop", throwIfNotFound: true);
        m_Blocks_HoldPiece = m_Blocks.FindAction("HoldPiece", throwIfNotFound: true);
        m_Blocks_SoftDropUp = m_Blocks.FindAction("SoftDropUp", throwIfNotFound: true);
        m_Blocks_SoftDropDown = m_Blocks.FindAction("SoftDropDown", throwIfNotFound: true);
        // Ui
        m_Ui = asset.FindActionMap("Ui", throwIfNotFound: true);
        m_Ui_TryAgain = m_Ui.FindAction("TryAgain", throwIfNotFound: true);
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

    // Blocks
    private readonly InputActionMap m_Blocks;
    private IBlocksActions m_BlocksActionsCallbackInterface;
    private readonly InputAction m_Blocks_Right;
    private readonly InputAction m_Blocks_Left;
    private readonly InputAction m_Blocks_RotateLeft;
    private readonly InputAction m_Blocks_RotateRight;
    private readonly InputAction m_Blocks_SoftDropHold;
    private readonly InputAction m_Blocks_HardDrop;
    private readonly InputAction m_Blocks_HoldPiece;
    private readonly InputAction m_Blocks_SoftDropUp;
    private readonly InputAction m_Blocks_SoftDropDown;
    public struct BlocksActions
    {
        private @Input m_Wrapper;
        public BlocksActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Right => m_Wrapper.m_Blocks_Right;
        public InputAction @Left => m_Wrapper.m_Blocks_Left;
        public InputAction @RotateLeft => m_Wrapper.m_Blocks_RotateLeft;
        public InputAction @RotateRight => m_Wrapper.m_Blocks_RotateRight;
        public InputAction @SoftDropHold => m_Wrapper.m_Blocks_SoftDropHold;
        public InputAction @HardDrop => m_Wrapper.m_Blocks_HardDrop;
        public InputAction @HoldPiece => m_Wrapper.m_Blocks_HoldPiece;
        public InputAction @SoftDropUp => m_Wrapper.m_Blocks_SoftDropUp;
        public InputAction @SoftDropDown => m_Wrapper.m_Blocks_SoftDropDown;
        public InputActionMap Get() { return m_Wrapper.m_Blocks; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BlocksActions set) { return set.Get(); }
        public void SetCallbacks(IBlocksActions instance)
        {
            if (m_Wrapper.m_BlocksActionsCallbackInterface != null)
            {
                @Right.started -= m_Wrapper.m_BlocksActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_BlocksActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_BlocksActionsCallbackInterface.OnRight;
                @Left.started -= m_Wrapper.m_BlocksActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_BlocksActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_BlocksActionsCallbackInterface.OnLeft;
                @RotateLeft.started -= m_Wrapper.m_BlocksActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.performed -= m_Wrapper.m_BlocksActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.canceled -= m_Wrapper.m_BlocksActionsCallbackInterface.OnRotateLeft;
                @RotateRight.started -= m_Wrapper.m_BlocksActionsCallbackInterface.OnRotateRight;
                @RotateRight.performed -= m_Wrapper.m_BlocksActionsCallbackInterface.OnRotateRight;
                @RotateRight.canceled -= m_Wrapper.m_BlocksActionsCallbackInterface.OnRotateRight;
                @SoftDropHold.started -= m_Wrapper.m_BlocksActionsCallbackInterface.OnSoftDropHold;
                @SoftDropHold.performed -= m_Wrapper.m_BlocksActionsCallbackInterface.OnSoftDropHold;
                @SoftDropHold.canceled -= m_Wrapper.m_BlocksActionsCallbackInterface.OnSoftDropHold;
                @HardDrop.started -= m_Wrapper.m_BlocksActionsCallbackInterface.OnHardDrop;
                @HardDrop.performed -= m_Wrapper.m_BlocksActionsCallbackInterface.OnHardDrop;
                @HardDrop.canceled -= m_Wrapper.m_BlocksActionsCallbackInterface.OnHardDrop;
                @HoldPiece.started -= m_Wrapper.m_BlocksActionsCallbackInterface.OnHoldPiece;
                @HoldPiece.performed -= m_Wrapper.m_BlocksActionsCallbackInterface.OnHoldPiece;
                @HoldPiece.canceled -= m_Wrapper.m_BlocksActionsCallbackInterface.OnHoldPiece;
                @SoftDropUp.started -= m_Wrapper.m_BlocksActionsCallbackInterface.OnSoftDropUp;
                @SoftDropUp.performed -= m_Wrapper.m_BlocksActionsCallbackInterface.OnSoftDropUp;
                @SoftDropUp.canceled -= m_Wrapper.m_BlocksActionsCallbackInterface.OnSoftDropUp;
                @SoftDropDown.started -= m_Wrapper.m_BlocksActionsCallbackInterface.OnSoftDropDown;
                @SoftDropDown.performed -= m_Wrapper.m_BlocksActionsCallbackInterface.OnSoftDropDown;
                @SoftDropDown.canceled -= m_Wrapper.m_BlocksActionsCallbackInterface.OnSoftDropDown;
            }
            m_Wrapper.m_BlocksActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @RotateLeft.started += instance.OnRotateLeft;
                @RotateLeft.performed += instance.OnRotateLeft;
                @RotateLeft.canceled += instance.OnRotateLeft;
                @RotateRight.started += instance.OnRotateRight;
                @RotateRight.performed += instance.OnRotateRight;
                @RotateRight.canceled += instance.OnRotateRight;
                @SoftDropHold.started += instance.OnSoftDropHold;
                @SoftDropHold.performed += instance.OnSoftDropHold;
                @SoftDropHold.canceled += instance.OnSoftDropHold;
                @HardDrop.started += instance.OnHardDrop;
                @HardDrop.performed += instance.OnHardDrop;
                @HardDrop.canceled += instance.OnHardDrop;
                @HoldPiece.started += instance.OnHoldPiece;
                @HoldPiece.performed += instance.OnHoldPiece;
                @HoldPiece.canceled += instance.OnHoldPiece;
                @SoftDropUp.started += instance.OnSoftDropUp;
                @SoftDropUp.performed += instance.OnSoftDropUp;
                @SoftDropUp.canceled += instance.OnSoftDropUp;
                @SoftDropDown.started += instance.OnSoftDropDown;
                @SoftDropDown.performed += instance.OnSoftDropDown;
                @SoftDropDown.canceled += instance.OnSoftDropDown;
            }
        }
    }
    public BlocksActions @Blocks => new BlocksActions(this);

    // Ui
    private readonly InputActionMap m_Ui;
    private IUiActions m_UiActionsCallbackInterface;
    private readonly InputAction m_Ui_TryAgain;
    public struct UiActions
    {
        private @Input m_Wrapper;
        public UiActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @TryAgain => m_Wrapper.m_Ui_TryAgain;
        public InputActionMap Get() { return m_Wrapper.m_Ui; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UiActions set) { return set.Get(); }
        public void SetCallbacks(IUiActions instance)
        {
            if (m_Wrapper.m_UiActionsCallbackInterface != null)
            {
                @TryAgain.started -= m_Wrapper.m_UiActionsCallbackInterface.OnTryAgain;
                @TryAgain.performed -= m_Wrapper.m_UiActionsCallbackInterface.OnTryAgain;
                @TryAgain.canceled -= m_Wrapper.m_UiActionsCallbackInterface.OnTryAgain;
            }
            m_Wrapper.m_UiActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TryAgain.started += instance.OnTryAgain;
                @TryAgain.performed += instance.OnTryAgain;
                @TryAgain.canceled += instance.OnTryAgain;
            }
        }
    }
    public UiActions @Ui => new UiActions(this);
    private int m_BlocksSchemeIndex = -1;
    public InputControlScheme BlocksScheme
    {
        get
        {
            if (m_BlocksSchemeIndex == -1) m_BlocksSchemeIndex = asset.FindControlSchemeIndex("Blocks");
            return asset.controlSchemes[m_BlocksSchemeIndex];
        }
    }
    public interface IBlocksActions
    {
        void OnRight(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRotateLeft(InputAction.CallbackContext context);
        void OnRotateRight(InputAction.CallbackContext context);
        void OnSoftDropHold(InputAction.CallbackContext context);
        void OnHardDrop(InputAction.CallbackContext context);
        void OnHoldPiece(InputAction.CallbackContext context);
        void OnSoftDropUp(InputAction.CallbackContext context);
        void OnSoftDropDown(InputAction.CallbackContext context);
    }
    public interface IUiActions
    {
        void OnTryAgain(InputAction.CallbackContext context);
    }
}
