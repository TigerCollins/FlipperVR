using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField]
    bool debugMode = false;

    [Header("Capabilities")]
    [SerializeField]
    PortalLocation targetPortal;
    [SerializeField]
    bool canTeleportPlayer = true;
    [SerializeField]
    bool canTeleportObject = true;

    [Header("Portal Link")]

    [SerializeField]
    Transform portalRoomTeleportPoint;
    [SerializeField]
    Transform labTeleportPoint, parkTeleportPoint, rooftopTeleportPoint, spaceTeleportPoint, debugModeTeleportPoint;

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

    public void Teleport(GameObject teleportObject)
    {
        if (teleportObject.CompareTag("Player") && canTeleportPlayer)
        {

        }

        else if (canTeleportObject)
        {
            TeleportObject(teleportObject);
        }

        else
        {
            Debug.LogError("Cannot teleport object");
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
                    if (portalRoomTeleportPoint != null)
                    {
                        teleportObject.transform.position = portalRoomTeleportPoint.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * portalRoomTeleportPoint.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find portal room");
                    }

                    break;
                case PortalLocation.Lab:
                    if (labTeleportPoint != null)
                    {
                        teleportObject.transform.position = labTeleportPoint.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * labTeleportPoint.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find lab portal");
                    }

                    break;
                case PortalLocation.Park:
                    if (parkTeleportPoint != null)
                    {
                        teleportObject.transform.position = parkTeleportPoint.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * parkTeleportPoint.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find park portal");
                    }

                    break;
                case PortalLocation.Rooftop:
                    if (rooftopTeleportPoint != null)
                    {
                        teleportObject.transform.position = rooftopTeleportPoint.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * rooftopTeleportPoint.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find rooftop portal");
                    }

                    break;
                case PortalLocation.Space:
                    if (spaceTeleportPoint != null)
                    {
                        teleportObject.transform.position = spaceTeleportPoint.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * spaceTeleportPoint.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find space portal");
                    }

                    break;
                case PortalLocation.DebugMode:
                    if (debugModeTeleportPoint != null)
                    {
                        teleportObject.transform.position = debugModeTeleportPoint.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * debugModeTeleportPoint.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find debug portal");
                    }

                    break;
                default:
                    Debug.LogError("Could not find any teleporter reference");
                    relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * debugModeTeleportPoint.transform.rotation;
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
