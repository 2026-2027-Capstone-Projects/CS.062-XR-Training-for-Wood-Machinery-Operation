using UnityEngine;

[CreateAssetMenu(fileName = "TrainingStep", menuName = "Scriptable Objects/TrainingStep")]
public class TrainingStep : ScriptableObject
{
    public int prevID; // ID of the previous training step
    public int ID; // unique ID of this training step
    public int nextID; // ID of the next training step

    public string stepDescription;
    public bool stepCompleted = false;



}
