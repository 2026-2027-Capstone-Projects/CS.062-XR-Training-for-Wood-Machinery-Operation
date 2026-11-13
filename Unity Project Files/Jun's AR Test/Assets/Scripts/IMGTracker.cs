
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager trackedImageManager;

    private void Update()
    {
        if (trackedImageManager == null)
            return;

        foreach (ARTrackedImage image in trackedImageManager.trackables)
        {
            ReportImage(image);
        }
    }

    private void ReportImage(ARTrackedImage image)
    {
        if (image.trackingState == TrackingState.Tracking)
        {
            Debug.Log(
                $"IMAGE TRACKED: {image.referenceImage.name} | " +
                $"Position: {image.transform.position} | " +
                $"Rotation: {image.transform.eulerAngles}"
            );
        }
        else if (image.trackingState == TrackingState.Limited)
        {
            Debug.Log($"IMAGE TRACKING LIMITED: {image.referenceImage.name}");
        }
    }
}
