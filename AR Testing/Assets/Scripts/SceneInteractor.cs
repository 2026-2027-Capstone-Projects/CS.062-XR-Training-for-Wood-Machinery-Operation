
using UnityEngine;

public class SceneInteractor : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionRange = 3f;

    void OnEnable()
    {
        if (inputHandler != null)
            inputHandler.SceneTapped += HandleTap;
    }

    void OnDisable()
    {
        if (inputHandler != null)
            inputHandler.SceneTapped -= HandleTap;
    }

    private void HandleTap(Vector2 screenPosition)
    {
        Ray ray =
            playerCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactionRange))
        {
            MachineInteractable interactable =
                hit.collider.GetComponentInParent<MachineInteractable>();

            if (interactable != null)
                interactable.Interact();
        }
    }
}
