using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/// <summary>
/// Mobile AR touch input. Raycasts from the screen tap into the scene and,
/// if an Interactable was hit, calls its OnInteract() callback.
///
/// This reuses the team's existing Interactable / PressButton architecture
/// instead of adding a second input path.
///
/// Attach this to the XR Origin GameObject in the AR scene.
/// Works with both the new Input System and the legacy Input Manager.
/// </summary>
public class ARTouchInteractor : MonoBehaviour
{
    [Tooltip("Max raycast distance in meters.")]
    [SerializeField] private float maxRayDistance = 10f;

    [Tooltip("Seconds to ignore further taps after one is handled.")]
    [SerializeField] private float cooldown = 0.4f;

    [Tooltip("Leave empty to use Camera.main.")]
    [SerializeField] private Camera arCamera;

    private float lastInteractTime = -999f;

    private void Start()
    {
        if (arCamera == null)
        {
            arCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (arCamera == null)
        {
            arCamera = Camera.main;
            if (arCamera == null) return;
        }

        if (Time.time - lastInteractTime < cooldown) return;

        if (!TryGetTapPosition(out Vector2 screenPosition)) return;

        Ray ray = arCamera.ScreenPointToRay(screenPosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, maxRayDistance)) return;

        // Interactable may sit on the hit object or on a parent of it.
        Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
        if (interactable == null) return;

        lastInteractTime = Time.time;

        Debug.Log($"[ARTouchInteractor] Tapped {hit.collider.name}, calling OnInteract().");

        // This is the callback function from the user story.
        interactable.OnInteract();
    }

    private bool TryGetTapPosition(out Vector2 screenPosition)
    {
        screenPosition = default;

#if ENABLE_INPUT_SYSTEM
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            return true;
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            screenPosition = Mouse.current.position.ReadValue();
            return true;
        }

        return false;
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began) return false;
            screenPosition = touch.position;
            return true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            screenPosition = Input.mousePosition;
            return true;
        }

        return false;
#endif
    }
}
