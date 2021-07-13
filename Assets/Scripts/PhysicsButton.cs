using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField]
    bool debugMode;

    [Space(5)]
    [Header("Physics")]
    [SerializeField]
    float threshold = .1f;
    [SerializeField]
    float deadZone = .025f;

    [Header("Target")]
    [SerializeField]
    int timesHit;
    [SerializeField]
    int hitTarget = 5;



    bool _isPressed;
    Vector3 _startPos;
    ConfigurableJoint _joint;

    [SerializeField]
    UnityEvent onPressed, onReleased,onHitTargetReached;
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.localPosition;
        if (TryGetComponent(out ConfigurableJoint newJoint))
        {
            _joint = newJoint;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }

        if(_isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }

    float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;

        if(Mathf.Abs(value) < deadZone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1, 1);
    }

    void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
        HitCount = HitCount + 1;
        if(debugMode)
        Debug.Log("Button has been pressed!");
    }

    void Released()
    {
        _isPressed = false;
        onReleased.Invoke();
        if (debugMode)
            Debug.Log("Button has been released!");
    }

    public int HitCount
    {
        get
        {
            return timesHit;
        }
        set
        {
            timesHit = value;
            if (debugMode)
            {
                Debug.Log("Hit the button " + timesHit + " times!");
            }
               

            if (timesHit >= hitTarget)
            {
                timesHit = 0;

                onHitTargetReached.Invoke();
               
            }
        
        }
    }
}
