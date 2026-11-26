using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public MachineComponent machineComponent;

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
        }
        else if(mode == TrainingMode.Assesment && machineComponent.Interactable)
        {
            Debug.Log("Interaction With: " + machineComponent.name);
        }

    }

}