using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField]
    TeleportManager teleportManager;
    [SerializeField]
    Camera playerCamera;

    [Header("Score")]
    [SerializeField]
    internal int objectsFlipped;
    [SerializeField]
    int currentAreaMultiplier;

    [Header("other")]
    [SerializeField]
    AreaDetails[] areaDetails;
    [SerializeField]
    TeleportManager.PortalLocation currentLocation;
    [Header("Controllers")]
    [SerializeField]
    internal ControllerDisplay leftController;
     [SerializeField]
    internal ControllerDisplay rightController;
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
                currentAreaMultiplier = areaDetails[i].areaMultiplier;
                if(areaDetails[i].desiredSkybox != null)
                {
                    RenderSettings.skybox = areaDetails[i].desiredSkybox;
                }
                break;
            }
        }
    }

    public void IncreaseScore()
    {
        objectsFlipped += 1 * currentAreaMultiplier;
        NewScore(objectsFlipped);
    }

    public void NewScore(int score)
    {
        rightController.scoreDisplay.text = score.ToString();
    }
}

[System.Serializable]
public class AreaDetails
{
    public string name;
    public Vector3 customGravityValue;
    public int areaMultiplier;
    public Material desiredSkybox;
}
