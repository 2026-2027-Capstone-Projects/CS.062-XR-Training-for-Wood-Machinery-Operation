using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Spawns a 3D button at the location of a tracked QR code image.
///
/// Written for AR Foundation 6.x (Unity 6).
/// Attach this to the GameObject that has ARTrackedImageManager,
/// which is normally "XR Origin".
/// </summary>
[RequireComponent(typeof(ARTrackedImageManager))]
public class QRButtonSpawner : MonoBehaviour
{
    [Tooltip("The prefab that contains the 3D button. It needs a Collider and the ARButton3D script.")]
    [SerializeField] private GameObject buttonPrefab;

    [Tooltip("Offset from the center of the QR code, in meters. Y points out of the paper.")]
    [SerializeField] private Vector3 localOffset = new Vector3(0f, 0.03f, 0f);

    [Tooltip("Hide the button when tracking of the QR code is lost.")]
    [SerializeField] private bool hideWhenNotTracking = true;

    private ARTrackedImageManager trackedImageManager;
    private readonly Dictionary<TrackableId, GameObject> spawnedButtons =
        new Dictionary<TrackableId, GameObject>();

    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        // AR Foundation 6.x uses a UnityEvent called trackablesChanged.
        trackedImageManager.trackablesChanged.AddListener(OnTrackablesChanged);
    }

    private void OnDisable()
    {
        trackedImageManager.trackablesChanged.RemoveListener(OnTrackablesChanged);
    }

    private void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (ARTrackedImage image in args.added)
        {
            SpawnButton(image);
        }

        foreach (ARTrackedImage image in args.updated)
        {
            UpdateButton(image);
        }

        // In AR Foundation 6.x, "removed" is a list of key/value pairs.
        foreach (KeyValuePair<TrackableId, ARTrackedImage> pair in args.removed)
        {
            RemoveButton(pair.Key);
        }
    }

    private void SpawnButton(ARTrackedImage image)
    {
        if (buttonPrefab == null)
        {
            Debug.LogError("[QRButtonSpawner] buttonPrefab is not assigned in the Inspector.");
            return;
        }

        if (spawnedButtons.ContainsKey(image.trackableId))
        {
            return;
        }

        // Parenting to the tracked image lets ARKit keep the button
        // anchored to the QR code for us, every frame.
        GameObject button = Instantiate(buttonPrefab, image.transform);
        button.transform.localPosition = localOffset;
        button.transform.localRotation = Quaternion.identity;

        spawnedButtons.Add(image.trackableId, button);

        Debug.Log($"[QRButtonSpawner] Spawned button for image: {image.referenceImage.name}");
    }

    private void UpdateButton(ARTrackedImage image)
    {
        if (!spawnedButtons.TryGetValue(image.trackableId, out GameObject button))
        {
            // Can happen if the image was first reported in a limited tracking state.
            SpawnButton(image);
            return;
        }

        if (hideWhenNotTracking)
        {
            bool isTracking = image.trackingState == TrackingState.Tracking;
            if (button.activeSelf != isTracking)
            {
                button.SetActive(isTracking);
            }
        }
    }

    private void RemoveButton(TrackableId id)
    {
        if (spawnedButtons.TryGetValue(id, out GameObject button))
        {
            Destroy(button);
            spawnedButtons.Remove(id);
        }
    }
}
