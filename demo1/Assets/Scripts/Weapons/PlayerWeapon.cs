using UnityEngine;
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
    private Vector3 finalOffset;

    private void Start() {
        if (weapon) UpdateWeapon(weapon);
        defaultPos = transform.localPosition;
        input = GetComponentInParent<MovementInput>();
    }
    private void Update()
    {
        // Look sway
        float x = input.defaultActions.Look.ReadValue<Vector2>().x;
        float y = input.defaultActions.Look.ReadValue<Vector2>().y;
        Vector3 lookOffset = new(
            x * swayAmount,
            y * swayAmount,
            0
        );
        finalOffset = Vector3.Lerp(finalOffset, lookOffset, Time.deltaTime * swaySmooth);
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