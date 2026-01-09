// *** Implementation for movement performed on the CharacterController ***
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Stats")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float crouchSpeed = 3f;
    [SerializeField] private float jumpHeight = 1f;
    private float currentSpeed;
    private float currentJumpHeight;
    private bool canRun;
    private float crouchTimer = 0f;
    private bool crouchTriggered = false;
    private bool isCrouching = false;
    
    [Header("World Physics")]
    [SerializeField] private float gravityAmount = 9.81f;
    [SerializeField] private float airResistance;
    private CharacterController controller;
    private Vector3 gravity;
    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = speed;
        gravity = new(0, -gravityAmount, 0);
    }
    private void Update()
    {
        ApplyPhysics();
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
        Vector3 direction = new()
        {
            x = input.x,
            z = input.y
        };
        controller.Move(currentSpeed * Time.deltaTime * transform.TransformDirection(direction));
    }
    public void Launch(Vector3 target, float time)
    {
        Vector3 start = transform.position;
        velocity = (target - start - 0.5f * Mathf.Pow(time, 2) * gravity) / time;
        controller.Move(velocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2f * currentJumpHeight * -gravityAmount);
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
    private void ApplyPhysics()
    {
        // Air Resistance
        velocity = Vector3.Lerp(velocity, Vector3.zero, airResistance * Time.deltaTime);
        // Gravity
        velocity += gravity * Time.deltaTime;
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        // Apply Movement
        controller.Move(velocity * Time.deltaTime);
    }
}