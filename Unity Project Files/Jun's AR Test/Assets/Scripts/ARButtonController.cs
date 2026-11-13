
using System.Collections.Generic;
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
    [SerializeField] private ARRaycastManager raycastManager;

    [Header("Button")]
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Vector3 buttonLocalOffset = new Vector3(0, 0, 0.05f);

    [Header("Animation")]
    [SerializeField] private ParticleSystem explosionPrefab;

    [Header("Camera")]
    [SerializeField] private Camera arCamera;

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
        if (!trackedImageManager || !raycastManager ||
            !buttonPrefab || !arCamera)
        {
            return;
        }

        if (!buttonInstance)
        {
            spawnButton();
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

    private void spawnButton()
    {
        foreach (ARTrackedImage image in trackedImageManager.trackables)
        {
            if (image.trackingState != TrackingState.Tracking)
            {
                continue;
            }

            buttonInstance = Instantiate(buttonPrefab, image.transform);

            buttonInstance.transform.localPosition = buttonLocalOffset;
            buttonInstance.transform.localRotation = Quaternion.identity;

            Debug.Log("AR Button spawned!");
            return;
        }
    }

    private void checkButtonTap(Vector2 touchPosition)
    {
        // perform a physics raycast against the button.
        Ray ray = arCamera.ScreenPointToRay(touchPosition);

        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            return;
        }

        if (hit.transform != buttonInstance.transform &&
            !hit.transform.IsChildOf(buttonInstance.transform))
        {
            return;
        }

        Debug.Log("Button pressed!");

        playExplosion(hit.point);
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
