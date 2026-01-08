using UnityEngine;
public class PlayerWeapon : MonoBehaviour
{
    [Header("Weapon Object")]
    [SerializeField] private GameObject weapon;
    [Header("UI Elements")]
    private Weapon currentWeapon;
    private GameObject weaponInstance;

    private void Start() {
        if (weapon) UpdateWeapon(weapon);
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