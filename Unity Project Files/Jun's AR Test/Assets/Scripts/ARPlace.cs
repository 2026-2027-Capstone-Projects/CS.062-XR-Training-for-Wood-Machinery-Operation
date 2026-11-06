using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ARPlace : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private bool allowMultiplePlacements = false;
    [SerializeField] private GameObject objectToPlace;

    private bool isPlaced = false;

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
        if (!raycastManager)
        {
            return;
        }

        if (!allowMultiplePlacements && isPlaced)
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

        placeObject(touch.screenPosition);
    }

    public void placeObject(Vector2 touchPosition)
    {
        if (!raycastManager)
        {
            return;
        }

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
            isPlaced = true;
        }
    }
}
