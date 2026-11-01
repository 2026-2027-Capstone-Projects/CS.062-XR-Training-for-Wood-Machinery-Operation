using UnityEngine;

public class PressButton : Interactable
{
    bool _pressed = false;

    public override void OnInteract()
    {
        base.OnInteract();
        _pressed = !_pressed;

        if (_pressed)
            transform.position = new Vector3(0, -2, 0);
        else 
            transform.position = new Vector3(0, 0, 0);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
