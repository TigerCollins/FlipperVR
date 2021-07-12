using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField]
    Transform gameCamera;
    Vector3 lookPos;

    // Start is called before the first frame update
    void Start()
    {
        gameCamera = FindObjectOfType<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        lookPos = gameCamera.position - transform.position;
        lookPos.x = 0;
        lookPos.z = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 50);
    }
}
