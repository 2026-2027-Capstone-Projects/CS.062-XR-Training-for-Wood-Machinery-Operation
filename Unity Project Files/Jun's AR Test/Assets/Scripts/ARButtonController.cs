
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ARButtonController : MonoBehaviour
{
    [Header("AR Managers")]
    [SerializeField] private ARTrackedImageManager trackedImageManager;

    [Header("Benny")]
    [SerializeField] private GameObject bennyPrefab;
    [SerializeField] private string bennyImg;

    [SerializeField] private Canvas instructionCanvas;
    [SerializeField] private TMP_Text instructionText;
    [SerializeField]
    private string[] instructions =
    {
        "Step 1: First instruction goes here.",
        "Step 2: Second instruction goes here.",
        "Step 3: Third instruction goes here."
    };
    private int currInstructionIndex = 0;

    [Header("Button")]
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private string buttonImg;
    [SerializeField] private Vector3 buttonLocalOffset = new Vector3(0, 0, 0.05f);

    [Header("Animation")]
    [SerializeField] private ParticleSystem explosionPrefab;

    [Header("Camera")]
    [SerializeField] private Camera arCamera;

    private GameObject bennyInstance;
    private GameObject buttonInstance;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        if (trackedImageManager)
        {
            if (!bennyInstance && bennyPrefab)
            {
                bennyInstance = spawnPrefabOnImage(bennyPrefab, bennyImg, Vector3.zero);
                if (bennyInstance)
                {
                    enableBennyCanvas();
                }
            }

            if (!buttonInstance && buttonPrefab)
            {
                buttonInstance = spawnPrefabOnImage(buttonPrefab, buttonImg, buttonLocalOffset);
            }
        }

        if (!arCamera)
        {
            return;
        }

        if (Touch.activeTouches.Count == 0)
        {
            return;
        }

        Touch touch = Touch.activeTouches[0];

        if (touch.phase != UnityEngine.InputSystem.TouchPhase.Began)
        {
            return;
        }

        checkButtonTap(touch.screenPosition);
    }

    private GameObject spawnPrefabOnImage(GameObject prefab, string imageName, Vector3 localOffset)
    {
        if (string.IsNullOrEmpty(imageName))
        {
            return null;
        }

        foreach (ARTrackedImage image in trackedImageManager.trackables)
        {
            if (image.trackingState != TrackingState.Tracking ||
                image.referenceImage.name != imageName)
            {
                continue;
            }

            GameObject instance = Instantiate(prefab, image.transform);

            instance.transform.localPosition = localOffset;
            instance.transform.localRotation = Quaternion.identity;

            Debug.Log($"{prefab.name} spawned on tracked image {imageName}.");
            return instance;
        }

        return null;
    }

    private void checkButtonTap(Vector2 touchPosition)
    {
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        RaycastHit targetHit = default;
        bool targetHitFound = false;
        bool hitBenny = false;

        foreach (RaycastHit hit in hits)
        {
            bool hitButton = buttonInstance &&
                (hit.transform == buttonInstance.transform ||
                 hit.transform.IsChildOf(buttonInstance.transform));
            bool hitBennyObject = bennyInstance &&
                (hit.transform == bennyInstance.transform ||
                 hit.transform.IsChildOf(bennyInstance.transform));

            if ((!hitButton && !hitBennyObject) ||
                (targetHitFound && hit.distance >= targetHit.distance))
            {
                continue;
            }

            targetHit = hit;
            targetHitFound = true;
            hitBenny = hitBennyObject;
        }

        if (!targetHitFound)
        {
            return;
        }

        if (!hitBenny)
        {
            Debug.Log("Button pressed!");
        }
        else
        {
            Debug.Log("Benny pressed!");
        }

        playExplosion(targetHit.point);
        if (hitBenny)
        {
            flipBenny();
        }
    }

    private void enableBennyCanvas()
    {
        if (instructionCanvas)
        {
            instructionCanvas.enabled = true;
        }
    }

    private void flipBenny()
    {
        if (!bennyInstance || !instructionText || instructions == null || instructions.Length == 0)
        {
            return;
        }

        currInstructionIndex = (currInstructionIndex + 1) % instructions.Length;
        instructionText.text = instructions[currInstructionIndex];
    }

    private void playExplosion(Vector3 position)
    {
        if (!explosionPrefab)
        {
            Debug.LogWarning("No explosion prefab assigned!");
            return;
        }

        ParticleSystem explosion = Instantiate(
            explosionPrefab,
            position,
            Quaternion.identity
        );

        explosion.Play();

        float lifetime = explosion.main.duration +
                         explosion.main.startLifetime.constantMax;

        Destroy(explosion.gameObject, lifetime + 0.5f);
    }
}
