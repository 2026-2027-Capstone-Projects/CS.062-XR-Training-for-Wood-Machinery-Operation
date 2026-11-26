using UnityEngine;
using System.Collections.Generic;

public class PhotoManager : MonoBehaviour
{

    [SerializeField] GameObject LeftNav;
    [SerializeField] GameObject RightNav;
    [SerializeField] GameObject UpNav;
    [SerializeField] GameObject DownNav;

    public PhotoObject currentPhotoObject;
    public SpriteRenderer image;

    [SerializeField]
    PhotoCameraController cameraController;

    [SerializeField] GameObject prefabInteractable;

    [SerializeField]
    List<GameObject> interactableObjects = new List<GameObject>();

    void Start()
    {
        LoadPhoto(currentPhotoObject);
    }

    public void LoadPhoto(PhotoObject photo)
    {
        if (photo == null)
            return;

        ClearCurrentInteractables();

        currentPhotoObject = photo;
        image.sprite = currentPhotoObject.photo;

        PopulateInteractables();
        UpdateNavigation();

        cameraController.ResetView();

    }

    private void UpdateNavigation()
    {
        LeftNav.SetActive(currentPhotoObject.left != null);
        RightNav.SetActive(currentPhotoObject.right != null);
        UpNav.SetActive(currentPhotoObject.up != null);
        DownNav.SetActive(currentPhotoObject.down != null);
    }

    public void ClearCurrentInteractables()
    {
        foreach (GameObject interactable in interactableObjects)
        {
            interactable.SetActive(false);
            Destroy(interactable);
        }

        interactableObjects.Clear();
    }

    public void PopulateInteractables()
    {
        if (currentPhotoObject.machineComponents == null)
            return;

        foreach (var component in currentPhotoObject.machineComponents)
        {
            GameObject newObject = Instantiate(
                prefabInteractable,
                component.componentPosition,
                Quaternion.Euler(component.componentRotation)
            );

            newObject.transform.localScale =
                component.componentScale;

            newObject.GetComponent<InteractableObject>()
                .machineComponent = component;

            newObject.name = component.name;

            interactableObjects.Add(newObject);
        }
    }

    public void LeftNavCallback()
    {
        LoadPhoto(currentPhotoObject.left);
    }

    public void RightNavCallback()
    {
        LoadPhoto(currentPhotoObject.right);
    }

    public void UpNavCallback()
    {
        LoadPhoto(currentPhotoObject.up);
    }

    public void DownNavCallback()
    {
        LoadPhoto(currentPhotoObject.down);
    }
}