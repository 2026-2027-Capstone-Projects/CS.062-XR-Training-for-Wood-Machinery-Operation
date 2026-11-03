using UnityEngine;
using UnityEngine.InputSystem;


public class InputHandler : MonoBehaviour
{

    InputSystem_Actions inputSystem_Actions;

    public Vector2 movementInput;
    public Vector2 cameraInput;
    public bool buttonHold;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (inputSystem_Actions == null)
        {
            inputSystem_Actions = new InputSystem_Actions();
            inputSystem_Actions.Player.Move.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputSystem_Actions.Player.Look.performed += i => cameraInput = i.ReadValue<Vector2>();
            inputSystem_Actions.Player.RightClick.performed += i => buttonHold = true;
            inputSystem_Actions.Player.RightClick.canceled += i => buttonHold = false;
        }

        inputSystem_Actions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(movementInput.ToString());
        Debug.Log(cameraInput.ToString());
    }
}
