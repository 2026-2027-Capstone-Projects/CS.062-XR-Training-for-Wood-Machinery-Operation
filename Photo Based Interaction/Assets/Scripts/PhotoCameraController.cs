using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

[RequireComponent(typeof(Camera))]
public class PhotoCameraController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer trainingImage;

    [SerializeField] private float maxZoom = 5f;
    [SerializeField] private float zoomSensitivity = 0.0015f;
    [SerializeField] private float panThreshold = 20f;

    private Camera cam;
    private float fitSize;

    private bool wasPinching;
    private float previousDistance;

    private bool singleFingerPanning;
    private int singleFingerId = -1;

    private Vector2 touchStart;
    private Vector2 previousTouchPosition;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        ResetGestureState();
    }

    private void Update()
    {
        if (trainingImage == null ||
            trainingImage.sprite == null ||
            fitSize <= 0f)
        {
            return;
        }

        if (Touch.activeTouches.Count > 0)
        {
            HandleTouch();
        }
        else
        {
            ResetGestureState();
            HandleMouse();
        }
    }

    private void HandleMouse()
    {
        Mouse mouse = Mouse.current;

        if (mouse == null)
            return;

        // Right-click and drag to pan.
        if (mouse.rightButton.isPressed &&
            !mouse.rightButton.wasPressedThisFrame)
        {
            Pan(mouse.delta.ReadValue());
        }

        // Scroll wheel to zoom.
        float scroll = mouse.scroll.ReadValue().y;

        if (scroll != 0f)
        {
            float factor = Mathf.Exp(
                -scroll * zoomSensitivity
            );

            Zoom(factor, mouse.position.ReadValue());
        }

        // Reset camera view.
        if (Keyboard.current != null &&
            Keyboard.current.homeKey.wasPressedThisFrame)
        {
            ResetView();
        }
    }

    private void HandleTouch()
    {
        var touches = Touch.activeTouches;

        // Single-finger panning.
        if (touches.Count == 1 && touches[0].isInProgress)
        {
            wasPinching = false;

            HandleSingleFinger(touches[0]);
            return;
        }

        singleFingerPanning = false;
        singleFingerId = -1;

        // Two-finger pinch zoom only.
        if (touches.Count == 2 &&
            touches[0].isInProgress &&
            touches[1].isInProgress)
        {
            HandlePinch(touches[0], touches[1]);
            return;
        }

        wasPinching = false;
    }

    private void HandleSingleFinger(Touch touch)
    {
        Vector2 position = touch.screenPosition;

        // Initialize a new gesture.
        if (touch.touchId != singleFingerId ||
            touch.phase == TouchPhase.Began)
        {
            singleFingerId = touch.touchId;

            touchStart = position;
            previousTouchPosition = position;

            singleFingerPanning = false;
            return;
        }

        // Distinguish a drag from an interaction tap.
        if (!singleFingerPanning)
        {
            float distance = Vector2.Distance(
                touchStart,
                position
            );

            if (distance > panThreshold)
            {
                singleFingerPanning = true;
            }
        }

        if (singleFingerPanning)
        {
            Pan(position - previousTouchPosition);
        }

        previousTouchPosition = position;
    }

    private void HandlePinch(Touch firstTouch, Touch secondTouch)
    {
        Vector2 first = firstTouch.screenPosition;
        Vector2 second = secondTouch.screenPosition;

        Vector2 center = (first + second) / 2f;
        float distance = Vector2.Distance(first, second);

        // Initialize a new pinch gesture.
        if (firstTouch.phase == TouchPhase.Began ||
            secondTouch.phase == TouchPhase.Began)
        {
            wasPinching = false;
        }

        if (wasPinching && distance > 1f)
        {
            float factor = previousDistance / distance;

            // Only zoom when finger separation changes.
            if (!Mathf.Approximately(factor, 1f))
            {
                Zoom(factor, center);
            }
        }

        previousDistance = distance;
        wasPinching = distance > 1f;
    }

    public void Pan(Vector2 screenDelta)
    {
        float unitsPerPixel =
            (cam.orthographicSize * 2f) / cam.pixelHeight;

        Vector3 movement = new Vector3(
            -screenDelta.x * unitsPerPixel,
            -screenDelta.y * unitsPerPixel,
            0f
        );

        transform.position += movement;

        ClampPosition();
    }

    public void Zoom(float factor, Vector2 screenPosition)
    {
        Vector3 before = ScreenToWorld(screenPosition);

        cam.orthographicSize = Mathf.Clamp(
            cam.orthographicSize * factor,
            fitSize / Mathf.Max(1f, maxZoom),
            fitSize
        );

        Vector3 after = ScreenToWorld(screenPosition);

        // Keep the zoom target beneath the cursor or pinch.
        transform.position += before - after;

        ClampPosition();
    }

    private Vector3 ScreenToWorld(Vector2 screenPosition)
    {
        float depth =
            trainingImage.bounds.center.z -
            transform.position.z;

        return cam.ScreenToWorldPoint(
            new Vector3(
                screenPosition.x,
                screenPosition.y,
                depth
            )
        );
    }

    public void ResetView()
    {
        if (trainingImage == null ||
            trainingImage.sprite == null)
        {
            return;
        }

        Bounds bounds = trainingImage.bounds;

        // Calculate the camera size required to fit the image.
        fitSize = Mathf.Max(
            bounds.extents.y,
            bounds.extents.x / cam.aspect
        );

        cam.orthographicSize = fitSize;

        transform.position = new Vector3(
            bounds.center.x,
            bounds.center.y,
            transform.position.z
        );

        ResetGestureState();
    }

    private void ClampPosition()
    {
        Bounds bounds = trainingImage.bounds;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        Vector3 position = transform.position;

        position.x = halfWidth >= bounds.extents.x
            ? bounds.center.x
            : Mathf.Clamp(
                position.x,
                bounds.min.x + halfWidth,
                bounds.max.x - halfWidth
            );

        position.y = halfHeight >= bounds.extents.y
            ? bounds.center.y
            : Mathf.Clamp(
                position.y,
                bounds.min.y + halfHeight,
                bounds.max.y - halfHeight
            );

        transform.position = position;
    }

    private void ResetGestureState()
    {
        wasPinching = false;
        previousDistance = 0f;

        singleFingerPanning = false;
        singleFingerId = -1;
    }
}