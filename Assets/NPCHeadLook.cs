using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCHeadLook : MonoBehaviour
{

    [SerializeField]
    Transform lookPoint;
    [SerializeField]
    Transform playerCam;
    [SerializeField]
    TeleportManager teleportManager;

    [Header("Quip")]
    [SerializeField]
    string[] possibleQuips;
    [SerializeField]
    TextMeshPro dialgoueText;
    int ranNum;

    // Start is called before the first frame update
    void Start()
    {
        ranNum = Random.Range(0, possibleQuips.Length);
        LookAtPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        if(teleportManager.currentPortal == TeleportManager.PortalLocation.portalRoom)
        {
            lookPoint.position = playerCam.position;
        }
    }

    public void NewQuip()
    {
        ranNum = Random.Range(0, possibleQuips.Length);
        dialgoueText.text = possibleQuips[ranNum];
    }
}
