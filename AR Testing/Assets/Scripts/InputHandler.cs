
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

using InputTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using InputTouchPhase = UnityEngine.InputSystem.TouchPhase;

public class InputHandler : MonoBehaviour
{
    private InputSystem_Actions inputSystem_Actions;

    public Vector2 movementInput;
    public Vector2 cameraInput;
    public Vector2 mobileLookDelta;
    public bool buttonHold;

    // Fired when the user taps or clicks the 3D scene.
    public event Action<Vector2> SceneTapped;

    [Header("Touch Settings")]
    [SerializeField] private float dragThreshold = 15f;
    [SerializeField] private float maxTapDuration = 0.3f;

    private int activeTouchId = -1;
    private Vector2 touchStartPosition;
    private float touchStartTime;
    private bool isDragging;

    public bool IsGamepadLook =>
        inputSystem_Actions.Player.Look.activeControl?.device
        is Gamepad;

    void Awake()
    {
        inputSystem_Actions = new InputSystem_Actions();

        inputSystem_Actions.Player.Move.performed +=
            i => movementInput = i.ReadValue<Vector2>();

        inputSystem_Actions.Player.Move.canceled +=
            i => movementInput = Vector2.zero;

        inputSystem_Actions.Player.Look.performed +=
            i => cameraInput = i.ReadValue<Vector2>();

        inputSystem_Actions.Player.Look.canceled +=
            i => cameraInput = Vector2.zero;

        inputSystem_Actions.Player.RightClick.performed +=
            i => buttonHold = true;

        inputSystem_Actions.Player.RightClick.canceled +=
            i => buttonHold = false;
    }

    void OnEnable()
    {
        inputSystem_Actions.Enable();
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        inputSystem_Actions.Disable();
        EnhancedTouchSupport.Disable();

        movementInput = Vector2.zero;
        cameraInput = Vector2.zero;
        mobileLookDelta = Vector2.zero;
        buttonHold = false;
        activeTouchId = -1;
    }

    void OnDestroy()
    {
        inputSystem_Actions.Dispose();
    }

    void Update()
    {
        mobileLookDelta = Vector2.zero;

        HandleTouches();
        HandleMouseClick();
    }

    private void HandleTouches()
    {
        foreach (InputTouch touch in InputTouch.activeTouches)
        {
            // Start tracking a finger only if it begins
            // outside the UI.
            if (touch.phase == InputTouchPhase.Began)
            {
                if (activeTouchId != -1)
                    continue;

                if (IsOverUI(touch.screenPosition))
                    continue;

                activeTouchId = touch.touchId;
                touchStartPosition = touch.screenPosition;
                touchStartTime = Time.unscaledTime;
                isDragging = false;

                continue;
            }

            // Ignore every finger except the one being
            // used for camera movement or scene interaction.
            if (touch.touchId != activeTouchId)
                continue;

            if (touch.phase == InputTouchPhase.Moved)
            {
                float distance = Vector2.Distance(
                    touch.screenPosition,
                    touchStartPosition
                );

                if (distance > dragThreshold)
                    isDragging = true;

                if (isDragging)
                    mobileLookDelta += touch.delta;
            }

            if (touch.phase == InputTouchPhase.Ended)
            {
                float duration =
                    Time.unscaledTime - touchStartTime;

                if (!isDragging &&
                    duration <= maxTapDuration &&
                    !IsOverUI(touch.screenPosition))
                {
                    SceneTapped?.Invoke(touch.screenPosition);
                }

                activeTouchId = -1;
                isDragging = false;
            }
            else if (touch.phase == InputTouchPhase.Canceled)
            {
                activeTouchId = -1;
                isDragging = false;
            }
        }
    }

    private void HandleMouseClick()
    {
        if (Mouse.current == null)
            return;

        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        Vector2 position =
            Mouse.current.position.ReadValue();

        if (!IsOverUI(position))
            SceneTapped?.Invoke(position);
    }

    private bool IsOverUI(Vector2 screenPosition)
    {
        if (EventSystem.current == null)
            return false;

        PointerEventData data =
            new PointerEventData(EventSystem.current);

        data.position = screenPosition;

        List<RaycastResult> results =
            new List<RaycastResult>();

        EventSystem.current.RaycastAll(data, results);

        foreach (RaycastResult result in results)
        {
            if (result.module is GraphicRaycaster)
                return true;
        }

        return false;
    }
}
