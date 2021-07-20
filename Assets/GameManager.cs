using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField]
    TeleportManager teleportManager;

    [Header("other")]
    [SerializeField]
    AreaDetails[] areaDetails;
    [SerializeField]
    TeleportManager.PortalLocation currentLocation;
    // Start is called before the first frame update


    private void Awake()
    {
 
    }

    public void ChangeAreaStats()
    {
        for (int i = 0; i < areaDetails.Length; i++)
        {
            if(areaDetails[i].name==teleportManager.targetPortal.ToString())
            {
                Physics.gravity = areaDetails[i].customGravityValue;
                break;
            }
        }
    }
}

[System.Serializable]
public class AreaDetails
{
    public string name;
    public bool customGravity = false;
    public Vector3 customGravityValue;
}
