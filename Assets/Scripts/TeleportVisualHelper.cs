using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class TeleportVisualHelper : MonoBehaviour
{
    [SerializeField]
    XRRayInteractor nonDominantController;
    [SerializeField]
    bool canShow;

    [SerializeField]
    bool isTeleportMovement;
    public Transform XRCamera;

    [Space(5)]

    [SerializeField]
    XRInteractorLineVisual teleportLineRenderer;
    [SerializeField]
    TeleportationArea[] teleportationArea;
    [SerializeField]
    PortalInteractor[] portalInteractors;


    Coroutine hideCoroutine;

    [SerializeField]
    Gradient movementGradient;
     [SerializeField]
    Gradient portalGradient;
    [SerializeField]
    Gradient invalidGradient;

    //[HideInInspector]
    public bool isSelectingPortal;







    public bool SetTeleportType
    {
        get
        {
            return isTeleportMovement;
        }

        set
        {
            isTeleportMovement = value;
            if(isTeleportMovement)
            {
                teleportLineRenderer.validColorGradient = movementGradient;
            }

            else
            {
                teleportLineRenderer.validColorGradient = portalGradient;
            }
        }
    }

    private void Update()
    {
        if(nonDominantController.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if(hit.collider.TryGetComponent(out PortalInteractor portalInteractor))
            {
                if(isTeleportMovement && teleportLineRenderer.validColorGradient != invalidGradient)
                {
                    teleportLineRenderer.validColorGradient = invalidGradient;
                }

                else if(portalInteractor.IsTriggerPressed == true && isSelectingPortal != true)
                {
                    portalInteractor.ShowPortalOutline();
                    isSelectingPortal = true;
                }

            }

            else if (isSelectingPortal)
            {
                isSelectingPortal = false;
            }

            if (hit.collider.TryGetComponent(out TeleportationArea teleportationArea))
            {
                if (isTeleportMovement && teleportLineRenderer.validColorGradient != movementGradient)
                {
                    teleportLineRenderer.validColorGradient = movementGradient;
                }
            }




        }

        else if(isSelectingPortal)
        {
            isSelectingPortal = false;
        }
    }

    public void Start()
    {

            canShow = false;
            for (int i = 0; i < teleportationArea.Length; i++)
            {
                if (teleportationArea[i].enabled == true)
                {
                    teleportationArea[i].enabled = false;

                }
            }
            teleportLineRenderer.enabled = false;
        
    }

    public void ShowTeleporter(InputAction.CallbackContext context)
    {
        if(!canShow && context.performed)
        {
            canShow = true;
            if(isTeleportMovement)
            {
                for (int i = 0; i < teleportationArea.Length; i++)
                {
                    if (teleportationArea[i].enabled == false)
                    {
                        teleportationArea[i].enabled = true;

                    }
                }
            }

            else
            {
                for (int i = 0; i < portalInteractors.Length; i++)
                {
                    if (portalInteractors[i].enabled == false)
                    {
                        portalInteractors[i].enabled = true;

                    }
                }
            }
           
            teleportLineRenderer.enabled = true;
        }
       
    }

    public void HideTeleporter(InputAction.CallbackContext context)
    {
        if (canShow && context.canceled && hideCoroutine == null)
        {
            canShow = false;
            hideCoroutine = StartCoroutine(HideTeleporter());
        }

    }

    public IEnumerator HideTeleporter()
    {
        yield return new WaitForSeconds(.01f);
        for (int i = 0; i < teleportationArea.Length; i++)
        {
            if (teleportationArea[i].enabled == true)
            {
                teleportationArea[i].enabled = false;

            }
        }
        for (int i = 0; i < portalInteractors.Length; i++)
        {
            if (portalInteractors[i].enabled == true)
            {
                portalInteractors[i].enabled = false;

            }
        }

        teleportLineRenderer.enabled = false;
        hideCoroutine = null;
    }

    public void Teleport()
    {
       // teleportRequest.destinationPosition
    }

}
