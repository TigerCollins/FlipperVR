using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PortalInteractor : XRBaseInteractable
{
    [Header("Visual")]
    [SerializeField]
    VisualEffect portalTitle;

    [Header("Teleportation")]
    [SerializeField]
    TeleportManager.PortalLocation targetPortalOverride = TeleportManager.PortalLocation.Null;
    [SerializeField]
    internal Transform objectTeleportPoint;
    [SerializeField]
    internal Transform playerTeleportPoint;

    [Header("Script References")]
    [SerializeField]
    TeleportManager teleportManager;
    [SerializeField]
    TeleportVisualHelper teleportVisualHelper;
    [SerializeField]
    Outline portalOutline;
    [SerializeField]
    XRInteractorLineVisual lineVisual;

    [Header("Teleportation Events")]
    [SerializeField]
    UnityEvent onPlayerInteract = new UnityEvent(); 
    [SerializeField]
    UnityEvent onObjectInteract;





    [Header("Input")]
    [SerializeField]
    InputActionProperty m_CustomTeleport;
    [Space(5)]
    [SerializeField]
    bool triggerPressedDown;


    //Hidden

    bool previousSelectionState;
    bool wasSelected;
    AudioSource playerAudioSource;

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

        m_CustomTeleport.action.performed += TriggerPressed;
        m_CustomTeleport.action.canceled += AttemptTeleport;
        m_CustomTeleport.action.canceled += CancelTeleport;
        m_CustomTeleport.action.canceled += TriggerPressed;
        onPlayerInteract.AddListener(delegate { teleportManager.Teleport(teleportManager.playerXRRig.gameObject); });
        previousSelectionState = teleportVisualHelper.isSelectingPortal;
        teleportManager = FindObjectOfType<TeleportManager>();
        playerAudioSource = FindObjectOfType<AudioSource>();
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
        SetPortalTitle();
    }

    public void SetPortalTitle()
    {
        if(portalTitle!=null)
        {
            switch (targetPortalOverride)
            {
                case TeleportManager.PortalLocation.portalRoom:
                    portalTitle.SetTexture("SDF Sprite (Distance Field)", teleportManager.portalRoomDistanceField);
                    portalTitle.SetVector3("Distance Field Vector3",new Vector3(18f, 11.2f, 1.01f));
                    break;
                case TeleportManager.PortalLocation.Lab:
                    portalTitle.SetTexture("SDF Sprite (Distance Field)", teleportManager.labPortalDistanceField); 
                    portalTitle.SetVector3("Distance Field Vector3", new Vector3(30.4f, 8.98f, 1.01f));
                    break;
                case TeleportManager.PortalLocation.Park:
                    portalTitle.SetTexture("SDF Sprite (Distance Field)", teleportManager.parkPortalDistanceField);
                    portalTitle.SetVector3("Distance Field Vector3", new Vector3(18.7f, 7.62f, 1.01f));
                    break;
                case TeleportManager.PortalLocation.Rooftop:
                    portalTitle.SetTexture("SDF Sprite (Distance Field)", teleportManager.rooftopPortalDistanceField);
                    portalTitle.SetVector3("Distance Field Vector3", new Vector3(29.2f, 8.4f, 1.01f));
                    break;
                case TeleportManager.PortalLocation.Space:
                    portalTitle.SetTexture("SDF Sprite (Distance Field)", teleportManager.spacePortalDistanceField);
                    portalTitle.SetVector3("Distance Field Vector3", new Vector3(23.39f, 8.98f, 1.01f));
                    break;
                case TeleportManager.PortalLocation.DebugMode:
                    portalTitle.SetTexture("SDF Sprite (Distance Field)", teleportManager.portalRoomDistanceField);
                    portalTitle.SetVector3("Distance Field Vector3", new Vector3(23.39f, 8.98f, 1.01f));
                    break;
                case TeleportManager.PortalLocation.Null:
                    switch (teleportManager.targetPortal)
                    {
                        case TeleportManager.PortalLocation.portalRoom:
                            portalTitle.SetTexture("SDF Sprite (Distance Field)", teleportManager.portalRoomDistanceField);
                            portalTitle.SetVector3("Distance Field Vector3", new Vector3(18f, 11.2f, 1.01f));
                            break;
                        case TeleportManager.PortalLocation.Lab:
                            portalTitle.SetTexture("SDF Sprite (Distance Field)", teleportManager.labPortalDistanceField);
                            portalTitle.SetVector3("Distance Field Vector3", new Vector3(30.4f, 8.98f, 1.01f));
                            break;
                        case TeleportManager.PortalLocation.Park:
                            portalTitle.SetTexture("SDF Sprite (Distance Field)", teleportManager.parkPortalDistanceField);
                            portalTitle.SetVector3("Distance Field Vector3", new Vector3(18.7f, 7.62f, 1.01f));
                            break;
                        case TeleportManager.PortalLocation.Rooftop:
                            portalTitle.SetTexture("SDF Sprite (Distance Field)", teleportManager.rooftopPortalDistanceField);
                            portalTitle.SetVector3("Distance Field Vector3", new Vector3(29.2f, 8.4f, 1.01f));
                            break;
                        case TeleportManager.PortalLocation.Space:
                            portalTitle.SetTexture("SDF Sprite (Distance Field)", teleportManager.spacePortalDistanceField);
                            portalTitle.SetVector3("Distance Field Vector3", new Vector3(23.39f, 8.98f, 1.01f));
                            break;
                        case TeleportManager.PortalLocation.DebugMode:
                            portalTitle.SetTexture("SDF Sprite (Distance Field)", teleportManager.portalRoomDistanceField);
                            portalTitle.SetVector3("Distance Field Vector3", new Vector3(23.39f, 8.98f, 1.01f));
                            break;
                        case TeleportManager.PortalLocation.Null:
                            portalTitle.SetVector3("Distance Field Vector3", new Vector3(23.39f, 8.98f, 1.01f));
                            portalTitle.SetTexture("SDF Sprite (Distance Field)", teleportManager.portalRoomDistanceField);

                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }
        }
        
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

        if (teleportVisualHelper.isSelectingPortal == false  || !IsTriggerPressed)//&& previousSelectionState != teleportVisualHelper.isSelectingPortal
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

       
   //    
        if (collision.gameObject.CompareTag("Player"))
        {
            if (targetPortalOverride != TeleportManager.PortalLocation.Null)
            {
                teleportManager.gameManager.currentLocation = TeleportManager.PortalLocation.portalRoom;
                teleportManager.ChangeTargetPortal(targetPortalOverride);
            }

            else
            {
                teleportManager.gameManager.currentLocation = teleportManager.targetPortal;
            }
            onPlayerInteract.Invoke();
        }

        else if(!collision.gameObject.CompareTag("Controller") && collision.gameObject.TryGetComponent(out FlippableObject flippable))
        {
            if(flippable.timesPickedUp !=0)
            {
                if (targetPortalOverride != TeleportManager.PortalLocation.Null)
                {
                    teleportManager.ChangeTargetPortal(targetPortalOverride);
                }
                teleportManager.Teleport(collision.gameObject);
                onObjectInteract.Invoke();
                flippable.timesPickedUp = 0;
            }
         
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
       
        if (isHovered && lineVisual.enabled)
        {
           
            TeleportManager.PortalLocation originalPortalLocation = teleportManager.targetPortal;
            if (targetPortalOverride != TeleportManager.PortalLocation.Null)
            {
                teleportManager.gameManager.currentLocation = TeleportManager.PortalLocation.portalRoom;
                teleportManager.ChangeTargetPortal(targetPortalOverride);
            }

            else
            {
                teleportManager.gameManager.currentLocation = teleportManager.targetPortal;
            }
            wasSelected = true;
            //teleportManager.gameManager.ChangeAreaStats(teleportManager.targetPortal);
            onPlayerInteract.Invoke();
            teleportManager.originalPortal = originalPortalLocation;
            
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
