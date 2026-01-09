// *** Implementation for movement performed on the CharacterController ***
using System.Collections;
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

    private bool ignoreAirRes;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = speed;
        gravity = new(0, -gravityAmount, 0);
    }
    private void Update()
    {
        // controlling physics & movement
        isGrounded = IsGrounded();
        ApplyPhysics();
        SpeedManager();
        //Debug.Log(isGrounded);
        //Debug.Log(velocity);

        // controlling lerp stuff
        CrouchLerp();
    }
    private bool IsGrounded()
    {
        float length = 1 + controller.skinWidth * 10;
        Ray ray = new(transform.position, -transform.up);
        return Physics.Raycast(ray, length);
    }
    private void ApplyPhysics()
    {
        // Air Resistance
        if (!isGrounded)
        {
            if (!ignoreAirRes) {
                velocity = Vector3.Lerp(velocity, Vector3.zero, airResistance * Time.deltaTime);
            }
        }
        else {
            velocity = Vector3.zero;
        }
        // Gravity
        velocity += gravity * Time.deltaTime;
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        // Apply Movement
        controller.Move(velocity * Time.deltaTime);
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
        StartCoroutine(LaunchRoutine(time));
    }
    private IEnumerator LaunchRoutine(float time)
    {
        ignoreAirRes = true;
        yield return new WaitForSeconds(time / 4);
        ignoreAirRes = false;
        Debug.Log("Air Resistance: ON");
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
    private void CrouchLerp()
    {
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