using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum TrainingMode
{
    Exploration, Guided, Assesment
}

public class InteractionModule : MonoBehaviour
{
    [SerializeField]
    TrainingMode mode = TrainingMode.Exploration;

    InputSystem_Actions inputSystem;

    [SerializeField]
    bool pressed = false;

    [SerializeField]
    LayerMask raycastMask;

    [SerializeField]
    GameObject selectedObject;


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

        inputSystem.Player.Attack.canceled += i => { pressed = false; };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pressed)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Vector2 mouse = Mouse.current.position.ReadValue();
            
            Ray ray = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;

            // 5. Perform the Physics Raycast
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastMask))
            {
                //Debug.Log($"Hit object: {hit.collider.gameObject.name} at position {hit.point}");
                if (selectedObject == hit.collider.gameObject && mode == TrainingMode.Guided)
                {
                    selectedObject = null;
                }
                else
                {
                    selectedObject = hit.collider.gameObject;
                    InteractableObject interactable = selectedObject.GetComponent<InteractableObject>();
                    if (interactable != null)
                    {
                        interactable.OnInteract(mode);
                    }
                }
                pressed = false; //Reset pressed so only 1 raycast is registered per click event

            }
        }
    }

}
