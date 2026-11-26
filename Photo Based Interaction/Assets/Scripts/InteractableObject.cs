using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public MachineComponent machineComponent;

    PhotoManager photoManager;

    private void Start()
    {
        photoManager = FindAnyObjectByType<PhotoManager>();
    }

    //Base interaction call
    public virtual void OnInteract(TrainingMode mode)
    {
        Debug.Log(mode);
        if (mode == TrainingMode.Exploration)
        {
            Debug.Log("Description: " + machineComponent.Description);
        }
        else if (mode == TrainingMode.Guided && machineComponent.Interactable)
        {
            Debug.Log("Interaction With: " + machineComponent.name);
            if (photoManager.currentPhotoObject.interactedState)
            {
                if (photoManager.image.sprite == photoManager.currentPhotoObject.photo)
                {
                    photoManager.image.sprite = photoManager.currentPhotoObject.interactedState;
                }
                else
                {
                    photoManager.image.sprite = photoManager.currentPhotoObject.photo;
                }
            }
        }
        else if(mode == TrainingMode.Assesment && machineComponent.Interactable)
        {
            Debug.Log("Interaction With: " + machineComponent.name);
            if (photoManager.currentPhotoObject.interactedState)
            {
                photoManager.image.sprite = photoManager.currentPhotoObject.interactedState;
            }
        }

    }

}