using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public MachineComponent machineComponent;

    public virtual void OnInteract(TrainingMode mode)
    {
        Debug.Log("Interaction With: " + machineComponent.name);
    }

}