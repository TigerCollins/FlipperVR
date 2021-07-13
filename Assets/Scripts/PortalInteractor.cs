using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PortalInteractor : XRBaseInteractable
{
    [SerializeField]
    TeleportVisualHelper teleportVisualHelper;
    [SerializeField]
    Outline portalOutline;
    [SerializeField]
    UnityEvent onPlayerInteract; 
    [SerializeField]
    UnityEvent onObjectInteract;
    [SerializeField]
    XRInteractorLineVisual lineVisual;
    [SerializeField]
    InputActionProperty m_CustomTeleport;

    [Space(5)]
    [SerializeField]
    bool triggerPressedDown;

    bool previousSelectionState;

    bool wasSelected;
    /// <summary>
    /// The Input System action to use for Position Tracking for this GameObject. Must be a <see cref="Vector3Control"/> Control.
    /// </summary>
    public InputActionProperty positionAction
    {
        get => m_CustomTeleport;
        set => SetInputActionProperty(ref m_CustomTeleport, value);
    }

    void SetInputActionProperty(ref InputActionProperty property, InputActionProperty value)
    {
        if (Application.isPlaying)
            property.DisableDirectAction();

        property = value;

        if (Application.isPlaying && isActiveAndEnabled)
            property.EnableDirectAction();
    }

    protected override void Awake()
    {
        m_CustomTeleport.action.performed += TriggerPressed ;
        m_CustomTeleport.action.canceled += AttemptTeleport;
        m_CustomTeleport.action.canceled += CancelTeleport;
        m_CustomTeleport.action.canceled += TriggerPressed;
        previousSelectionState = teleportVisualHelper.isSelectingPortal;
        if (portalOutline == null)
        {
            if (transform.parent.TryGetComponent(out Outline outline))
            {
                portalOutline = outline;
            }
               
        }
        base.Awake();
    }


    private void Start()
    {
        HidePortalOutline();
    }


    public void ShowPortalOutline()
    {
        portalOutline.enabled = true;
    }

    public void HidePortalOutline()
    {
        portalOutline.enabled = false; 
    }

    private void Update()
    {
        if (teleportVisualHelper.isSelectingPortal == false && previousSelectionState != teleportVisualHelper.isSelectingPortal || !IsTriggerPressed)
        {
            HidePortalOutline();
           
        }
        previousSelectionState = teleportVisualHelper.isSelectingPortal;
    }

    void TriggerPressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            triggerPressedDown = true;
        }

        else
        {
            triggerPressedDown = false;

        }
    }

    public bool IsTriggerPressed
    {
        get
        {
            return triggerPressedDown;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            onPlayerInteract.Invoke();
        }

        else
        {
            onObjectInteract.Invoke();
        }
      
    }

    private void OnCollisionStay(Collision collision)
    {
       /* if (collision.transform.tag == "Player")
        {
            onPlayerInteract.Invoke();
        }

        else
        {
            onObjectInteract.Invoke();
        }
       */
    }

    void AttemptTeleport(InputAction.CallbackContext context)
    {
        if(isHovered && lineVisual.enabled)
        {
            wasSelected = true;
            onPlayerInteract.Invoke();
        }
    }

    void CancelTeleport(InputAction.CallbackContext context)
    {
        if(wasSelected)
        {
            wasSelected = false;
        }
    }




    }
