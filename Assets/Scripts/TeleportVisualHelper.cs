using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class TeleportVisualHelper : MonoBehaviour
{
    [SerializeField]
    bool canShow;

    [Space(5)]

    [SerializeField]
    XRInteractorLineVisual teleportLineRenderer;
    [SerializeField]
    TeleportationArea[] teleportationArea;
    Coroutine hideCoroutine;


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

            for (int i = 0; i < teleportationArea.Length; i++)
            {
                if (teleportationArea[i].enabled == false)
                {
                    teleportationArea[i].enabled = true;

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
        teleportLineRenderer.enabled = false;
        hideCoroutine = null;
    }

    public void Teleport()
    {
       // teleportRequest.destinationPosition
    }

}
