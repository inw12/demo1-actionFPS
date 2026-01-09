// *** Handles player input for WEAPON-based actions ***
using UnityEngine;
using UnityEngine.InputSystem;
public class InteractionInput : MonoBehaviour
{
    private PlayerInput input;
    private PlayerInteract interactionController;

    private void Awake()
    {
        input = new PlayerInput();
        interactionController = GetComponent<PlayerInteract>();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Default.Interact.performed += OnInteractionTriggered;
    }
    private void OnDisable()
    {
        input.Default.Interact.performed -= OnInteractionTriggered;
        input.Disable();
    }

    private void OnInteractionTriggered(InputAction.CallbackContext ctx) => interactionController.Interact();
}
