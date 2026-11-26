using UnityEngine;

[CreateAssetMenu(fileName = "MachineComponent", menuName = "Scriptable Objects/MachineComponent")]
public class MachineComponent : ScriptableObject
{
    public string Name; // Name of the component
    public string Description; // Description of component showed to user

    public Vector3 componentPosition;
    public Vector3 componentRotation;
    public Vector3 componentScale;

    public bool Interactable;

}
