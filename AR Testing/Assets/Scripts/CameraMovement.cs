
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private InputHandler inputHandler;
    private GameObject cameraObject;

    [Header("Sensitivity")]
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float touchSensitivity = 0.15f;
    [SerializeField] private float gamepadSensitivity = 120f;

    [Header("Camera Smoothing")]
    [SerializeField] private float smoothingSpeed = 15f;

    [Header("Camera Limits")]
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;

    private float targetPitch;
    private float targetYaw;

    void Start()
    {
        cameraObject = Camera.main.gameObject;
        inputHandler = GetComponent<InputHandler>();

        // Initialize rotation from current camera orientation
        Vector3 rotation = cameraObject.transform.eulerAngles;

        targetPitch = Mathf.DeltaAngle(0f, rotation.x);
        targetYaw = rotation.y;
    }

    void LateUpdate()
    {
        // Mobile touchscreen camera movement
        Vector2 look = inputHandler.mobileLookDelta
                       * touchSensitivity;

        // Gamepad right stick
        if (inputHandler.IsGamepadLook)
        {
            look += inputHandler.cameraInput
                    * gamepadSensitivity
                    * Time.deltaTime;
        }
        // Mouse movement while holding right click
        else if (inputHandler.buttonHold)
        {
            look += inputHandler.cameraInput
                    * mouseSensitivity;
        }

        // Update target rotation
        targetYaw += look.x;
        targetPitch -= look.y;

        // Prevent camera from rotating vertically too far
        targetPitch = Mathf.Clamp(
            targetPitch,
            minPitch,
            maxPitch
        );

        Quaternion targetRotation = Quaternion.Euler(
            targetPitch,
            targetYaw,
            0f
        );

        // Smooth interpolation independent of frame rate
        float t = 1f - Mathf.Exp(
            -smoothingSpeed * Time.deltaTime
        );

        cameraObject.transform.rotation = Quaternion.Slerp(
            cameraObject.transform.rotation,
            targetRotation,
            t
        );
    }
}
