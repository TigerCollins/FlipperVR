using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class HandButton : XRBaseInteractable
{
    [Header("Hand Button")]
    [SerializeField]
    float inRangePressOffset = .01f;
    float yMin = 0f;
    float yMax = 0f;

    float previoushandHeight;
    bool previousPress = false;
    XRBaseInteractor hoverInteractor = null;

    [SerializeField]
    UnityEvent OnClick;

    protected override void Awake()
    {
        base.Awake();
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);
    }

    void OnDestroy()
    {
        onHoverEntered.RemoveListener(StartPress);
        onHoverExited.RemoveListener(EndPress);
    }

    void StartPress(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        previoushandHeight = GetLocalYPosition(hoverInteractor.transform.position);
    }

    void EndPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        previoushandHeight = 0;

        previousPress = false;
        SetYPosition(yMax);
    }

    private void Start()
    {
        SetMinMax();
    }

    void SetMinMax()
    {
        if(TryGetComponent(out Collider collider))
        {
            yMin = transform.localPosition.y - (collider.bounds.size.y * .5f);
            yMax = transform.localPosition.y;
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(hoverInteractor)
        {
            float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDifference = previoushandHeight - newHandHeight;
            previoushandHeight = newHandHeight;

            float newPosition = transform.localPosition.y - handDifference;
            SetYPosition(newPosition);

            CheckPress();
        }
    }

    float GetLocalYPosition(Vector3 position)
    {
        Vector3 localPosition = transform.root.InverseTransformPoint(position);


        return localPosition.y;
    }

    void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, yMin, yMax);
        transform.localPosition = newPosition;
    }

    void CheckPress()
    {
        bool inPositon = InPosition();

        if(inPositon && inPositon != previousPress)
        {
            OnClick.Invoke();
            previousPress = inPositon;
        }

    }

    bool InPosition()
    {
        float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + inRangePressOffset);

        return transform.localPosition.y == inRange ;
    }
}
