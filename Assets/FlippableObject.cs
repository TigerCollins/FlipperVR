using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;


[RequireComponent(typeof(XRGrabInteractable))]
public class FlippableObject : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    bool hasLanded;
    [SerializeField]
    bool isUpright;

    [Header("Settings")]
    [SerializeField]
    float uprightThreshold;

    [Header("Unity Events")]
    [SerializeField]
    UnityEvent onStartMovement;
     [SerializeField]
    UnityEvent onEndMovement;
    [SerializeField]
    UnityEvent onFinishedUpright;
    [SerializeField]
    UnityEvent didntFinishedUpright;



    //hidden
    bool previousLandedState;
    bool previousUprightState;


    private void Start()
    {
        if(TryGetComponent(out Rigidbody newRB))
        {
            rb = newRB;
        }
    }

    private void Update()
    {
        if(rb.IsSleeping() != previousLandedState)
        {
            previousLandedState = hasLanded;
            hasLanded = rb.IsSleeping();
            if(hasLanded)
            {
                onEndMovement.Invoke();
            }

            else
            {
                onStartMovement.Invoke();
            }
            
        }
       
        if (hasLanded && IsUpright() != previousUprightState)
        {
            previousUprightState = isUpright;
            isUpright = IsUpright();
            if (isUpright)
            {
                onFinishedUpright.Invoke();
            }

            else
            {
                didntFinishedUpright.Invoke();
            }
        }

        else if(isUpright)
        {
            isUpright = false;
        }
       
    }

    public bool IsUpright()
    {
        return transform.up.y > uprightThreshold;/*say 0.6 ?*/
    }




}
