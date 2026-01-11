using UnityEngine;
using UnityEngine.TextCore.Text;
public class PlayerWeapon : MonoBehaviour
{
    [Header("Weapon Object")]
    [SerializeField] private GameObject weapon;
    private Weapon currentWeapon;
    private GameObject weaponInstance;
    [Header("Weapon Sway")]
    public float swayAmount = 0.05f;
    public float swaySmooth = 8f;
    private MovementInput input;
    private Vector3 defaultPos;
    [Header("Weapon Bobbing")]
    public float bobAmount = 0.05f;
    public float bobSpeed = 6f;
    public float bobStopStrength = 5f;
    public float bobFrequency = 2f;

    private float bobTimer;
    private PlayerMovement moveController;
    private Vector3 targetOffset;
    private Vector3 finalOffset;

    private void Start() {
        if (weapon) UpdateWeapon(weapon);
        defaultPos = transform.localPosition;
        input = GetComponentInParent<MovementInput>();
        moveController = GetComponentInParent<PlayerMovement>();
    }
    private void Update()
    {
        // Look Sway
        float x = input.defaultActions.Look.ReadValue<Vector2>().x;
        float y = input.defaultActions.Look.ReadValue<Vector2>().y;
        Vector3 lookOffset = new(
            x * swayAmount,
            y * swayAmount,
            0
        );
        // Movement Bob
        float move = moveController.GetSpeed();
        if (moveController.IsGrounded() && move > 0.1f) {
            bobTimer += Time.deltaTime * bobSpeed;
        }
        else {
            bobTimer = Mathf.Lerp(bobTimer, 0, Time.deltaTime * bobStopStrength);
        }
        float bobX = Mathf.Sin(bobTimer) * bobAmount;
        float bobY = Mathf.Cos(bobTimer * bobFrequency) * bobAmount;
        Vector3 bobOffset = new(bobX, bobY, 0);

        targetOffset = lookOffset + bobOffset;
        finalOffset = Vector3.Lerp(finalOffset, targetOffset, Time.deltaTime * swaySmooth);
        transform.localPosition = defaultPos + finalOffset;
    }
    public void Attack(bool attackButtonDown) => currentWeapon.Attack(attackButtonDown);
    public void UpdateWeapon(GameObject newWeapon)
    {
        // Destroy current weapon object (if it exists)
        if (transform.childCount > 0) {
            Destroy(transform.GetChild(0).gameObject);
        }
        // Instantiate newly obtained weapon
        weaponInstance = Instantiate(newWeapon, transform);
        currentWeapon = weaponInstance.GetComponent<Weapon>();
        currentWeapon.Initialize(GetComponentInParent<PlayerLook>().cam);
    }
}