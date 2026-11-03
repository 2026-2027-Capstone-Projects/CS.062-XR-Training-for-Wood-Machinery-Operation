using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    InputHandler inputHandler;
    GameObject camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main.gameObject;
        inputHandler = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        float xRot = camera.transform.eulerAngles.x - inputHandler.cameraInput.y;
        float yRot = camera.transform.eulerAngles.y + inputHandler.cameraInput.x;
        if (inputHandler.buttonHold)
        {
            Vector3 rotation = new Vector3(xRot, yRot, 0);
            camera.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
