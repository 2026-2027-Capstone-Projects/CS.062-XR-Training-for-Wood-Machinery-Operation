using UnityEngine;

public abstract class MachineInteractable : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log("INTERACT");
    }
}
