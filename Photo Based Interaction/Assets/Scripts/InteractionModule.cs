using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public enum TrainingMode
{
    Exploration, Guided, Assesment
}

public class InteractionModule : MonoBehaviour
{
    [SerializeField] TrainingMode mode = TrainingMode.Exploration;
    [SerializeField] LayerMask raycastMask;
    [SerializeField] Camera interactionCamera;
    [SerializeField] GameObject selectedObject;

    [SerializeField] float tapThreshold = 20f;

    private Vector2 touchStart;
    private bool validTap;
    private bool suppressTouch;
    private int trackedTouchId;

    private readonly List<RaycastResult> uiHits = new();

    void Awake()
    {
        if (interactionCamera == null)
            interactionCamera = Camera.main;
    }

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();

        validTap = false;
        suppressTouch = false;
    }

    void LateUpdate()
    {
        if (Touch.activeTouches.Count > 0)
        {
            HandleTouch();
            return;
        }

        suppressTouch = false;
        validTap = false;

        HandleMouse();
    }

    private void HandleMouse()
    {
        Mouse mouse = Mouse.current;

        if (mouse == null)
            return;

        // Prevent interaction while panning.
        if (mouse.rightButton.isPressed)
            return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            TryInteract(mouse.position.ReadValue());
        }
    }

    private void HandleTouch()
    {
        var touches = Touch.activeTouches;

        // Cancel interaction during multi-touch gestures.
        if (touches.Count > 1)
        {
            validTap = false;
            suppressTouch = true;
            return;
        }

        // Wait until all fingers have been released.
        if (suppressTouch)
            return;

        Touch touch = touches[0];

        switch (touch.phase)
        {
            case TouchPhase.Began:

                touchStart = touch.screenPosition;
                trackedTouchId = touch.touchId;
                validTap = !IsPointerOverUI(touchStart);
                break;

            case TouchPhase.Moved:

                if (Vector2.Distance(
                    touchStart,
                    touch.screenPosition) > tapThreshold)
                {
                    validTap = false;
                }

                break;

            case TouchPhase.Ended:

                if (validTap &&
                    touch.touchId == trackedTouchId &&
                    Vector2.Distance(
                        touchStart,
                        touch.screenPosition) <= tapThreshold)
                {
                    TryInteract(touch.screenPosition);
                }

                validTap = false;
                break;

            case TouchPhase.Canceled:

                validTap = false;
                break;
        }
    }

    private void TryInteract(Vector2 screenPosition)
    {
        if (interactionCamera == null ||
            IsPointerOverUI(screenPosition))
        {
            return;
        }

        Ray ray = interactionCamera.ScreenPointToRay(
            screenPosition
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            Mathf.Infinity,
            raycastMask))
        {
            if (selectedObject == hit.collider.gameObject &&
                mode == TrainingMode.Guided)
            {
                selectedObject = null;
            }
            else
            {
                selectedObject = hit.collider.gameObject;

                InteractableObject interactable =
                    selectedObject.GetComponent<InteractableObject>();

                if (interactable != null)
                {
                    interactable.OnInteract(mode);
                }
            }
        }
    }

    private bool IsPointerOverUI(Vector2 screenPosition)
    {
        if (EventSystem.current == null)
            return false;

        PointerEventData pointer = new PointerEventData(
            EventSystem.current
        );

        pointer.position = screenPosition;

        uiHits.Clear();

        EventSystem.current.RaycastAll(pointer, uiHits);

        foreach (RaycastResult hit in uiHits)
        {
            if (hit.module is GraphicRaycaster)
                return true;
        }

        return false;
    }
}