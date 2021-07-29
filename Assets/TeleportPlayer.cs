using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField]
    TeleportManager teleportManager;
    [SerializeField]
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        teleportManager = FindObjectOfType<TeleportManager>();
        characterController = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        teleportManager.Teleport(characterController.gameObject);
    }

}
