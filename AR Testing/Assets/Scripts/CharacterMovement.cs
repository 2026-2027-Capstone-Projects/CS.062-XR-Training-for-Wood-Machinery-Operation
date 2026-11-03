using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMovement : MonoBehaviour
{
    Transform cameraObject;
    InputHandler inputHandler;
    public Vector3 moveDirection;
    Rigidbody rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        cameraObject = Camera.main.transform;
        rigidbody = GetComponent<Rigidbody>();
    }


    Vector3 normalVector;
    Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {

        moveDirection = cameraObject.forward * inputHandler.movementInput.y;
        moveDirection += cameraObject.right * inputHandler.movementInput.x;

        moveDirection.Normalize();
        moveDirection.y = 0;

        float speed = 5;
        moveDirection *= speed;

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.linearVelocity = projectedVelocity;
    }
}
