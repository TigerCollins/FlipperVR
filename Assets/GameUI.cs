using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    CanvasGroup canvasGroup;
    [SerializeField]
    TeleportManager teleportManager;

    [Space(10)]
    [SerializeField]
    bool isVisible = true;
    [SerializeField]
    CanvasGroup menuGroup;

    [SerializeField]
    InputActionProperty m_ToggleMenuLeft;
    [SerializeField]
    InputActionProperty m_ToggleMenuRight;
  //  InputActionProperty m_ToggleMenuRight;
    // Start is called before the first frame update
    void Start()
    {
        UpdateMenuVisibility();
        m_ToggleMenuLeft.action.performed += ToggleMenu;
        m_ToggleMenuRight.action.performed += ToggleMenu;
      //  m_ToggleMenuRight.action.performed += ToggleMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("objectsFlipped", 0);
        gameManager.objectsFlipped = PlayerPrefs.GetInt("objectsFlipped");
        gameManager.NewScore(gameManager.objectsFlipped);
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("poggers");
            isVisible = !isVisible;
            UpdateMenuVisibility();
        }
    }

    void UpdateMenuVisibility()
    {
        if(isVisible)
        {
            menuGroup.alpha = 1;
        }

        else
        {
            menuGroup.alpha = 0;
        }
    }

    public void QuitGame()
    {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }

    public IEnumerator TeleportCanvas(GameObject teleportObject)
    {
        while (canvasGroup.alpha<1)
        {
            canvasGroup.alpha += Time.deltaTime *2;
            yield return null;
        }
       
        teleportManager.ValidTeleportPlayer(teleportObject);
        
        if (teleportManager.originalPortal != teleportManager.targetPortal)
        {
              teleportManager.ChangeTargetPortal(teleportManager.originalPortal);
        }
        yield return new WaitForSeconds(.1f);
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 2;
            yield return null;
        }
    }
    
}
