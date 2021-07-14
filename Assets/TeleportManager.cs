using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportManager : MonoBehaviour
{
    [SerializeField]
    bool debugMode = false;
    [SerializeField]
    Transform playerXRRig;

    [Header("Capabilities")]
    [SerializeField]
    PortalLocation targetPortal;
    [SerializeField]
    bool canTeleportPlayer = true;
    [SerializeField]
    bool canTeleportObject = true;

    [Header("Portal Link - Object")]

    [SerializeField]
    Transform portalRoomTeleportPointObject;
    [SerializeField]
    Transform labTeleportPointObject, parkTeleportPointObject, rooftopTeleportPointObject, spaceTeleportPointObject, debugModeTeleportPointObject;

    [Header("Portal Link - Player")]

    [SerializeField]
    Transform portalRoomTeleportPointPlayer;
    [SerializeField]
    Transform labTeleportPointPlayer, parkTeleportPointPlayer, rooftopTeleportPointPlayer, spaceTeleportPointPlayer, debugModeTeleportPointPlayer;

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
                    if (portalRoomTeleportPointPlayer != null)
                    {
                        teleportObject.transform.position = portalRoomTeleportPointPlayer.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * portalRoomTeleportPointPlayer.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find portal room");
                    }

                    break;
                case PortalLocation.Lab:
                    if (labTeleportPointPlayer != null)
                    {
                        teleportObject.transform.position = labTeleportPointPlayer.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * labTeleportPointPlayer.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find lab portal");
                    }

                    break;
                case PortalLocation.Park:
                    if (parkTeleportPointPlayer != null)
                    {
                        teleportObject.transform.position = parkTeleportPointPlayer.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * parkTeleportPointPlayer.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find park portal");
                    }

                    break;
                case PortalLocation.Rooftop:
                    if (rooftopTeleportPointPlayer != null)
                    {
                        teleportObject.transform.position = rooftopTeleportPointPlayer.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * rooftopTeleportPointPlayer.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find rooftop portal");
                    }

                    break;
                case PortalLocation.Space:
                    if (spaceTeleportPointPlayer != null)
                    {
                        teleportObject.transform.position = spaceTeleportPointPlayer.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * spaceTeleportPointPlayer.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find space portal");
                    }

                    break;
                case PortalLocation.DebugMode:
                    if (debugModeTeleportPointPlayer != null)
                    {
                        teleportObject.transform.position = debugModeTeleportPointPlayer.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * debugModeTeleportPointPlayer.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find debug portal");
                    }

                    break;
                default:
                    Debug.LogError("Could not find any teleporter reference");
                    relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * debugModeTeleportPointPlayer.transform.rotation;
                break;
            }

        //  rigidbody.velocity = (relativeRotation * rigidbody.velocity * 2);
        Quaternion rot180degrees = Quaternion.Euler(-teleportObject.transform.rotation.eulerAngles);
            teleportObject.transform.rotation *= relativeRotation;



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
                    if (portalRoomTeleportPointObject != null)
                    {
                        teleportObject.transform.position = portalRoomTeleportPointObject.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * portalRoomTeleportPointObject.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find portal room");
                    }

                    break;
                case PortalLocation.Lab:
                    if (labTeleportPointObject != null)
                    {
                        teleportObject.transform.position = labTeleportPointObject.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * labTeleportPointObject.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find lab portal");
                    }

                    break;
                case PortalLocation.Park:
                    if (parkTeleportPointObject != null)
                    {
                        teleportObject.transform.position = parkTeleportPointObject.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * parkTeleportPointObject.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find park portal");
                    }

                    break;
                case PortalLocation.Rooftop:
                    if (rooftopTeleportPointObject != null)
                    {
                        teleportObject.transform.position = rooftopTeleportPointObject.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * rooftopTeleportPointObject.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find rooftop portal");
                    }

                    break;
                case PortalLocation.Space:
                    if (spaceTeleportPointObject != null)
                    {
                        teleportObject.transform.position = spaceTeleportPointObject.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * spaceTeleportPointObject.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find space portal");
                    }

                    break;
                case PortalLocation.DebugMode:
                    if (debugModeTeleportPointObject != null)
                    {
                        teleportObject.transform.position = debugModeTeleportPointObject.position;
                        relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * debugModeTeleportPointObject.transform.rotation;
                    }

                    else
                    {
                        Debug.LogError("Could not find debug portal");
                    }

                    break;
                default:
                    Debug.LogError("Could not find any teleporter reference");
                    relativeRotation = Quaternion.Inverse(teleportObject.transform.rotation) * debugModeTeleportPointObject.transform.rotation;
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
