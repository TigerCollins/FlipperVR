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
    TeleportManager teleportManager;
    [SerializeField]
    bool hasLanded;
    [SerializeField]
    bool isUpright;
    [SerializeField]
    int timesFlipped;
    [SerializeField]
    float accruedAngle;

    [Space(10)]
    [SerializeField]
    internal int timesPickedUp;

    [Header("Settings")]
    [SerializeField]
    float uprightThreshold = .8f;

    [Header("Audio")]
    [SerializeField]
    float minPitch = .7f;
    [SerializeField]
    float maxPitch = 1.2f;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip fireWorks;

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
    bool previousKinematicState;
  
    Quaternion lastRotation;

    private void Start()
    {
        if(TryGetComponent(out Rigidbody newRB))
        {
            rb = newRB;
        }

        if (TryGetComponent(out AudioSource newAudio))
        {
            audioSource = newAudio;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(teleportManager.currentPortal != TeleportManager.PortalLocation.Space && !collision.gameObject.CompareTag("Controller"))
        {
            PlaySound();
        }
    }
    void PlaySound()
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
    }

    private void Update()
    {
        CheckMovement();
        CheckUpRight();
        CheckKinematic();
        CountFlips();
    }

    public void CheckMovement()
    {
        if (rb.IsSleeping() != previousLandedState)
        {
            previousLandedState = rb.IsSleeping();
            hasLanded = rb.IsSleeping();
            if (hasLanded)
            {
                onEndMovement.Invoke();
            }

            else
            {
               
                onStartMovement.Invoke();
            }

        }
    }

    void CheckUpRight()
    {
        if (hasLanded && IsUpright() != previousUprightState)
        {
            previousUprightState = IsUpright();
            isUpright = IsUpright();
            if (isUpright && timesFlipped !=0)
            {
                onFinishedUpright.Invoke();
                audioSource.PlayOneShot(fireWorks);
        
            }

            else
            {
                didntFinishedUpright.Invoke();
              //  ResetFlip();
            }
        }

        else if (isUpright)
        {
            isUpright = false;
            //ResetFlip();
        }
    }

    void CheckKinematic()
    {
        if (rb.isKinematic != previousKinematicState)
        {
            previousKinematicState = rb.isKinematic;
            if (rb.isKinematic == true)
            {
                timesPickedUp ++;
                ResetFlip();// posWhenKinematic = transform.position;
            }

        }
    }

    public bool IsUpright()
    {
        if(rb.isKinematic == false)
        {
            return transform.up.y > uprightThreshold;/*say 0.6 ?*/
        }
        return false;
    }

    void ResetFlip()
    {
        timesFlipped = 0;
        accruedAngle = 0;
    }

    public void CountFlips()
    {
        if(!rb.isKinematic)
        {
            accruedAngle += Quaternion.Angle(transform.rotation, lastRotation);
            timesFlipped = (int)accruedAngle / 360;
            lastRotation = transform.rotation;
        }

    }



}
