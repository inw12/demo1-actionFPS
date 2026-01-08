// *** Implementation for movement from the player ***

using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float crouchSpeed = 3f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float gravity = -9.81f;
    private CharacterController controller;
    private float currentSpeed;
    private float currentJumpHeight;
    private Vector3 velocity;
    private bool isGrounded;
    private bool canRun;
    private float crouchTimer = 0f;
    private bool crouchTriggered = false;
    private bool isCrouching = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = speed;
    }
    private void Update()
    {
        isGrounded = controller.isGrounded;
        if (crouchTriggered)
        {
            crouchTimer += Time.deltaTime;
            float p = Mathf.Pow(crouchTimer, 2);
            float targetHeight = isCrouching ? 1 : 2;
            controller.height = Mathf.Lerp(controller.height, targetHeight, p);
            if (p > 1) {
                crouchTriggered = false;
            }
        }
        SpeedManager();
    }
    public void Move(Vector2 input)
    {
        // HORIZONTAL Movement
        Vector3 direction = new()
        {
            x = input.x,
            z = input.y
        };
        controller.Move(currentSpeed * Time.deltaTime * transform.TransformDirection(direction));
        // VERTICAL Movement
        velocity.y += gravity * Time.deltaTime;
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        controller.Move(velocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2f * currentJumpHeight * gravity);
            controller.Move(velocity * Time.deltaTime);
        }
    }
    public void Run(bool runButtonDown)
    {
        if (isGrounded) {
            canRun = runButtonDown;
        }
    }
    public void Crouch()
    {
        if (isGrounded)
        {
            isCrouching = !isCrouching;
            crouchTriggered = true;
            crouchTimer = 0f;
        }
    }
    private void SpeedManager()
    {
        if (isCrouching) {
            currentSpeed = crouchSpeed;
            currentJumpHeight = jumpHeight / 2;
        }
        else if (canRun && GetComponent<MovementInput>().defaultActions.Walk.ReadValue<Vector2>().y > 0) {
            currentSpeed = runSpeed;
        }
        else {
            currentSpeed = speed;
            currentJumpHeight = jumpHeight;
        }
    }
}
