using UnityEngine;

[CreateAssetMenu(fileName = "MachineComponent", menuName = "Scriptable Objects/MachineComponent")]
public class MachineComponent : ScriptableObject
{
    public string Name; // Name of the component
    public string Description; // Description of component showed to user
}
