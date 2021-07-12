using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDisplay : MonoBehaviour
{
    [SerializeField]
    bool isDominantHand;
    [SerializeField]
    bool isLeftHand;

    [Space(5)]

    [SerializeField]
    ControlSchemes currentControlSchemes;
    ControlSchemes previousControlScheme;


    public enum ControlSchemes
    {
        Traversal
    }


    public bool IsDominanthand
    {
        get
        {
            return isDominantHand;
        }

        set
        {
            isDominantHand = value;
            ChangeHand();
        }
    }

    public void ChangeHand()
    {
        if(isLeftHand && isDominantHand)
        {
            Debug.Log("Left hand is your dominant hand");
        }

        else if(isLeftHand)
        {
            Debug.Log("Left hand is NOT your dominant hand");
        }

        else if(!isLeftHand && isDominantHand)
        {
            Debug.Log("Right hand is your dominant hand");
        }
        else
        {
            Debug.Log("Right hand is NOT your dominant hand");
        }
       
    }

    private void Update()
    {
        if(previousControlScheme != currentControlSchemes)
        {
            previousControlScheme = currentControlSchemes;
            OnControlSchemeChange();
        }
    }
    public void OnControlSchemeChange()
    {

    }
}
