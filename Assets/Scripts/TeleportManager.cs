using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportManager : MonoBehaviour
{
    [SerializeField]
    bool debugMode = false;
    [SerializeField]
    internal GameObject playerXRRig;

    [Header("Capabilities")]
    [SerializeField]
    internal PortalLocation targetPortal;
    [SerializeField]
    bool canTeleportPlayer = true;
    [SerializeField]
    bool canTeleportObject = true;

    [Header("Portal Interactors")]
    [SerializeField]
    internal PortalInteractor portalRoomInteractor;
    [SerializeField]
    internal PortalInteractor labPortalInteractor, parkPortalInteractor, rooftopPortalInteractor, spacePortalInteractor, debugModePortalInteractor;

    public enum PortalLocation
    {
        portalRoom,
        Lab,
        Park,
        Rooftop,
        Space,
        DebugMode,
        Null
    }

   

    public void ChangeTargetPortal(PortalLocation newTargetPortal)
    {

        targetPortal = newTargetPortal;
        Debug.Log("Changed target portal to " + newTargetPortal.ToString());
    }

    public void ChangeTargetPortalString(string newTargetPortal)
    {

        targetPortal = (PortalLocation)System.Enum.Parse(typeof(PortalLocation), newTargetPortal); ;
        Debug.Log("Changed target portal to " + newTargetPortal.ToString());
    }

    public void Teleport(GameObject teleportObject)
    {
        if (teleportObject.CompareTag("Player") && canTeleportPlayer)
        {
            TeleportPlayer(teleportObject);
        }

        else if (canTeleportObject && !teleportObject.CompareTag("Controller"))
        {
            TeleportObject(teleportObject);
        }

        else
        {
            Debug.LogError("Cannot teleport object");
        }
    }

    void TeleportPlayer(GameObject teleportObject)
    {

            Quaternion relativeRotation = Quaternion.identity;
            switch (targetPortal)
            {
                case PortalLocation.portalRoom:
                    if (portalRoomInteractor.playerTeleportPoint != null)
                    {
                        teleportObject.transform.position = portalRoomInteractor.playerTeleportPoint.position;
                        relativeRotation = portalRoomInteractor.playerTeleportPoint.transform.localRotation; //Quaternion.Inverse(teleportObject.transform.rotation) * 
                }

                    else
                    {
                        Debug.LogError("Could not find portal room");
                    }

                    break;
                case PortalLocation.Lab:
                    if (labPortalInteractor.playerTeleportPoint != null)
                    {
                        teleportObject.transform.position = labPortalInteractor.playerTeleportPoint.position;
                        relativeRotation = labPortalInteractor.playerTeleportPoint.transform.localRotation; //Quaternion.Inverse(teleportObject.transform.rotation) * 
                }

                    else
                    {
                        Debug.LogError("Could not find lab portal");
                    }

                    break;
                case PortalLocation.Park:
                    if (parkPortalInteractor.playerTeleportPoint != null)
                    {
                        teleportObject.transform.position = parkPortalInteractor.playerTeleportPoint.position;
                        relativeRotation =  parkPortalInteractor.playerTeleportPoint.transform.localRotation;//Quaternion.Inverse(teleportObject.transform.rotation) *
                }

                    else
                    {
                        Debug.LogError("Could not find park portal");
                    }

                    break;
                case PortalLocation.Rooftop:
                    if (rooftopPortalInteractor.playerTeleportPoint != null)
                    {
                        teleportObject.transform.position = rooftopPortalInteractor.playerTeleportPoint.position;
                        relativeRotation = rooftopPortalInteractor.playerTeleportPoint.transform.localRotation;// Quaternion.Inverse(teleportObject.transform.rotation) *
                }

                    else
                    {
                        Debug.LogError("Could not find rooftop portal");
                    }

                    break;
                case PortalLocation.Space:
                    if (spacePortalInteractor != null)
                    {
                        teleportObject.transform.position = spacePortalInteractor.playerTeleportPoint.transform.position;
                        relativeRotation = spacePortalInteractor.playerTeleportPoint.transform.localRotation;//Quaternion.Inverse(teleportObject.transform.rotation) * 
                }

                    else
                    {
                        Debug.LogError("Could not find space portal");
                    }

                    break;
                case PortalLocation.DebugMode:
                    if (debugModePortalInteractor.playerTeleportPoint != null)
                    {
                        teleportObject.transform.position = debugModePortalInteractor.playerTeleportPoint.position;
                        relativeRotation = debugModePortalInteractor.playerTeleportPoint.transform.localRotation;//Quaternion.Inverse(teleportObject.transform.rotation) * 
                }

                    else
                    {
                        Debug.LogError("Could not find debug portal");
                    }

                    break;
                default:
                    Debug.LogError("Could not find any teleporter reference");
                    relativeRotation =  debugModePortalInteractor.playerTeleportPoint.transform.localRotation;//Quaternion.Inverse(teleportObject.transform.rotation) *
                break;
            }

        //  rigidbody.velocity = (relativeRotation * rigidbody.velocity * 2);
        Quaternion rot180degrees = Quaternion.Euler(-teleportObject.transform.rotation.eulerAngles);
            teleportObject.transform.rotation = relativeRotation;



        if (debugMode)
        {
            Debug.Log("Teleported " + teleportObject.name + " to the " + targetPortal.ToString());
        }
    }

    void TeleportObject(GameObject teleportObject)
    {
        if (teleportObject.TryGetComponent(out Rigidbody rigidbody))
        {
            
            Quaternion relativeRotation = Quaternion.identity;
            switch (targetPortal)
            {
                case PortalLocation.portalRoom:
                    if (portalRoomInteractor.objectTeleportPoint != null)
                    {
                        teleportObject.transform.position = portalRoomInteractor.objectTeleportPoint.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * portalRoomInteractor.objectTeleportPoint.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find portal room");
                    }

                    break;
                case PortalLocation.Lab:
                    if (labPortalInteractor.objectTeleportPoint != null)
                    {
                        teleportObject.transform.position = labPortalInteractor.objectTeleportPoint.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * labPortalInteractor.objectTeleportPoint.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find lab portal");
                    }

                    break;
                case PortalLocation.Park:
                    if (parkPortalInteractor.objectTeleportPoint != null)
                    {
                        teleportObject.transform.position = parkPortalInteractor.objectTeleportPoint.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * parkPortalInteractor.objectTeleportPoint.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find park portal");
                    }

                    break;
                case PortalLocation.Rooftop:
                    if (rooftopPortalInteractor.objectTeleportPoint != null)
                    {
                        teleportObject.transform.position = rooftopPortalInteractor.objectTeleportPoint.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * rooftopPortalInteractor.objectTeleportPoint.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find rooftop portal");
                    }

                    break;
                case PortalLocation.Space:
                    if (spacePortalInteractor.objectTeleportPoint != null)
                    {
                        teleportObject.transform.position = spacePortalInteractor.objectTeleportPoint.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * spacePortalInteractor.objectTeleportPoint.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find space portal");
                    }

                    break;
                case PortalLocation.DebugMode:
                    if (debugModePortalInteractor.objectTeleportPoint != null)
                    {
                        teleportObject.transform.position = debugModePortalInteractor.objectTeleportPoint.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * debugModePortalInteractor.objectTeleportPoint.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find debug portal");
                    }

                    break;
                default:
                    Debug.LogError("Could not find any teleporter reference");
                    relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * debugModePortalInteractor.objectTeleportPoint.transform.rotation;
                    break;
            }
           
            rigidbody.velocity = (relativeRotation * rigidbody.velocity * 2);
            teleportObject.transform.rotation *= relativeRotation;
        }

        if (debugMode)
        {
            Debug.Log("Teleported " + teleportObject.name + " to the " + targetPortal.ToString());
        }
    }
}
