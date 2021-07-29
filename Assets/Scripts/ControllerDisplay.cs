using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerDisplay : MonoBehaviour
{
    [SerializeField]
    bool isDominantHand;
    [SerializeField]
    bool isLeftHand;
    [SerializeField]
    internal TextMeshPro scoreDisplay;

    [Space(5)]

    [SerializeField]
    ControlSchemes currentControlSchemes;
    ControlSchemes previousControlScheme;

    [Header("Default Dominant")]
    [SerializeField]
    DefaultScheme defaultSchemeNonDominant; 
    [SerializeField]
    DefaultScheme defaultSchemeDominant;
    GameManager gameManager;
    //   [Header("Portal Usage")]

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if(transform.parent.parent.TryGetComponent(out ControllerDetails controllerDetails))
        {
            isLeftHand = controllerDetails.isLeftHand;

            //The set has an event trigger
            IsDominanthand = controllerDetails.isDominanthand;
          
        }
        ChangeHand();
        gameManager.NewScore(gameManager.objectsFlipped);
    }

    public enum ControlSchemes
    {
        Default
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
            gameManager.leftController = this;
        }

        else if(isLeftHand)
        {
            Debug.Log("Left hand is NOT your dominant hand");
            gameManager.leftController = this;
        }

        else if(!isLeftHand && isDominantHand)
        {
            Debug.Log("Right hand is your dominant hand");
            gameManager.rightController = this;
        }
        else
        {
            Debug.Log("Right hand is NOT your dominant hand");
            gameManager.rightController = this;
        }
        OnControlSchemeChange();
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
        switch (currentControlSchemes)
        {
            case ControlSchemes.Default:
                if (IsDominanthand)
                {
                    if (defaultSchemeDominant.triggerInput != null)
                    {
                        defaultSchemeDominant.triggerInput.SetActive(true);

                    }

                    if (defaultSchemeDominant.activateInput != null)
                    {
                        defaultSchemeDominant.activateInput.SetActive(true);

                    }
                    if (defaultSchemeDominant.menuInput != null)
                    {
                        defaultSchemeDominant.menuInput.SetActive(true);

                    }

                    if (defaultSchemeDominant.groupInput != null)
                    {
                        defaultSchemeDominant.groupInput.SetActive(true);

                    }

                    //Disables elements
                    if (defaultSchemeNonDominant.triggerInput != null)
                    {
                        defaultSchemeNonDominant.triggerInput.SetActive(false);

                    }

                    if (defaultSchemeNonDominant.activateInput != null)
                    {
                        defaultSchemeNonDominant.activateInput.SetActive(false);

                    }
                    if (defaultSchemeNonDominant.menuInput != null)
                    {
                        defaultSchemeNonDominant.menuInput.SetActive(false);

                    }

                    if (defaultSchemeNonDominant.groupInput != null)
                    {
                        defaultSchemeNonDominant.groupInput.SetActive(false);

                    }

                }

                else
                {
                    if (defaultSchemeNonDominant.triggerInput != null)
                    {
                        defaultSchemeNonDominant.triggerInput.SetActive(true);

                    }

                    if (defaultSchemeNonDominant.activateInput != null)
                    {
                        defaultSchemeNonDominant.activateInput.SetActive(true);

                    }
                    if (defaultSchemeNonDominant.menuInput != null)
                    {
                        defaultSchemeNonDominant.menuInput.SetActive(true);

                    }

                    if (defaultSchemeNonDominant.groupInput != null)
                    {
                        defaultSchemeNonDominant.groupInput.SetActive(true);

                    }

                    //Disables elements

                    if (defaultSchemeDominant.triggerInput != null)
                    {
                        defaultSchemeDominant.triggerInput.SetActive(false);

                    }

                    if (defaultSchemeDominant.activateInput != null)
                    {
                        defaultSchemeDominant.activateInput.SetActive(false);

                    }
                    if (defaultSchemeDominant.menuInput != null)
                    {
                        defaultSchemeDominant.menuInput.SetActive(false);

                    }

                    if (defaultSchemeDominant.groupInput != null)
                    {
                        defaultSchemeDominant.groupInput.SetActive(false);

                    }
                }
                    break;
            default:
                break;
        }
       
        
    }
}

[System.Serializable]
public class DominantHand
{

}

[System.Serializable]
public class DefaultScheme
{
    public GameObject triggerInput;
    public GameObject activateInput;
    public GameObject menuInput;
    public GameObject groupInput;
}
