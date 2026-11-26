using UnityEngine;

[CreateAssetMenu(fileName = "PhotoObject", menuName = "Scriptable Objects/PhotoObject")]
public class PhotoObject : ScriptableObject
{

    public string Name;
    public Sprite photo;

    //Relative positions of next images
    public PhotoObject left;
    public PhotoObject right;
    public PhotoObject up;
    public PhotoObject down;

    public Sprite interactedState;

    public MachineComponent[] machineComponents;


}
