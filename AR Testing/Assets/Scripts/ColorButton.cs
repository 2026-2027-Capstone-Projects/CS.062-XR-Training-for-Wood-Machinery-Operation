using UnityEngine;

public class ColorButton : MachineInteractable
{
    [SerializeField]
    string color;
    [SerializeField]
    Color colorValue;
    [SerializeField]
    Light light;
    [SerializeField]
    MeshRenderer lightPostBulb;
    Animator animator;

    public override void Interact()
    {
        base.Interact();
        Debug.Log(color);
        animator.Play("ButtonPress");
        light.color = colorValue;
        lightPostBulb.material.color = colorValue;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
