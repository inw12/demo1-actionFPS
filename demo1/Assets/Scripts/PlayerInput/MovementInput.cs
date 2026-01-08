// *** Handles player for MOVEMENT-based actions ***
using UnityEngine;
using UnityEngine.InputSystem;
public class MovementInput : MonoBehaviour
{
    private PlayerInput input;
    public PlayerInput.DefaultActions defaultActions;
    private PlayerMovement moveController;
    private PlayerLook lookController;

    private void Awake()
    {
        input = new PlayerInput();
        defaultActions = input.Default;
        moveController = GetComponent<PlayerMovement>();
        lookController = GetComponent<PlayerLook>();
    }
    private void OnEnable()
    {
        input.Enable();
        defaultActions.Jump.performed += OnJumpTriggered;
        defaultActions.Run.performed += OnRunTriggered;
        defaultActions.Run.canceled += OnRunTriggered;
        defaultActions.Crouch.performed += OnCrouchTriggered;
    }
    private void OnDisable()
    {
        defaultActions.Jump.performed -= OnJumpTriggered;
        defaultActions.Run.performed -= OnRunTriggered;
        defaultActions.Run.canceled -= OnRunTriggered;
        defaultActions.Crouch.performed -= OnCrouchTriggered;
        input.Disable();
    }

    private void Update()
    {
        moveController.Move(defaultActions.Walk.ReadValue<Vector2>());
        lookController.Look(defaultActions.Look.ReadValue<Vector2>());
    }

    private void OnJumpTriggered(InputAction.CallbackContext ctx) => moveController.Jump();
    private void OnRunTriggered(InputAction.CallbackContext ctx) => moveController.Run(ctx.performed);
    private void OnCrouchTriggered(InputAction.CallbackContext ctx) => moveController.Crouch();
}
