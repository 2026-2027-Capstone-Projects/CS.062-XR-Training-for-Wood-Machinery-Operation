using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMaster : MonoBehaviour
{

    InputSystem_Actions inputSystem;

    bool pressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (inputSystem == null)
        {
            inputSystem = new InputSystem_Actions();
        }
        inputSystem.Enable();


        inputSystem.Player.Attack.performed += context =>
        {
            pressed = true;
            
        };

        inputSystem.Player.Attack.canceled += i => { pressed = false; Debug.Log("Button Canceled");};
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pressed)
        {
            Vector2 mouse = Mouse.current.position.ReadValue();
            Debug.Log("Button Pressed");
            Ray ray = Camera.main.ScreenPointToRay(mouse);

            RaycastHit hit;

            // 5. Perform the Physics Raycast
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log($"Hit object: {hit.collider.gameObject.name} at position {hit.point}");
            }
        }
    }
}
