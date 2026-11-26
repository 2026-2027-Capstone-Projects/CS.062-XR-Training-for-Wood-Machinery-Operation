using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

[RequireComponent(typeof(Camera))]
public class PhotoCameraController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer trainingImage;

    [SerializeField] private float maxZoom = 5f;
    [SerializeField] private float zoomSensitivity = 0.0015f;

    private Camera cam;

    private float fitSize;
    private bool wasPinching;
    private Vector2 previousCenter;
    private float previousDistance;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        wasPinching = false;
    }

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
    }

    private void Start()
    {
        ResetView();
    }

    private void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            HandleTouch();
        }
        else
        {
            wasPinching = false;
            HandleMouse();
        }
    }

    private void HandleMouse()
    {
        Mouse mouse = Mouse.current;

        if (mouse == null)
            return;

        if (mouse.rightButton.isPressed &&
            !mouse.rightButton.wasPressedThisFrame)
        {
            Pan(mouse.delta.ReadValue());
        }

        float scroll = mouse.scroll.ReadValue().y;

        if (scroll != 0)
        {
            float factor = Mathf.Exp(
                -scroll * zoomSensitivity
            );

            Zoom(factor, mouse.position.ReadValue());
        }

        if (Keyboard.current != null &&
            Keyboard.current.homeKey.wasPressedThisFrame)
        {
            ResetView();
        }
    }

    private void HandleTouch()
    {
        var touches = Touch.activeTouches;

        if (touches.Count != 2 ||
            !touches[0].isInProgress ||
            !touches[1].isInProgress)
        {
            wasPinching = false;
            return;
        }

        Vector2 first = touches[0].screenPosition;
        Vector2 second = touches[1].screenPosition;

        Vector2 center = (first + second) / 2f;
        float distance = Vector2.Distance(first, second);

        if (touches[0].phase ==
                UnityEngine.InputSystem.TouchPhase.Began ||
            touches[1].phase ==
                UnityEngine.InputSystem.TouchPhase.Began)
        {
            wasPinching = false;
        }

        if (wasPinching && distance > 1f)
        {
            Pan(center - previousCenter);

            float factor = previousDistance / distance;

            Zoom(factor, center);
        }

        previousCenter = center;
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

        transform.position += before - after;

        ClampPosition();
    }

    private Vector3 ScreenToWorld(Vector2 screenPosition)
    {
        float depth = trainingImage.bounds.center.z
                      - transform.position.z;

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
        Bounds bounds = trainingImage.bounds;

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
}