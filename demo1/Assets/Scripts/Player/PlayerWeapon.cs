using TMPro;
using UnityEngine;
public class PlayerWeapon : MonoBehaviour
{
    [Header("Weapon Object")]
    [SerializeField] private GameObject weapon;
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI magText;
    [SerializeField] private TextMeshProUGUI ammoText;
    private Weapon currentWeapon;
    private GameObject weaponInstance;

    private void Start() {
        if (weapon) UpdateWeapon(weapon);
    }
    public void Attack(bool attackButtonDown) => currentWeapon.Attack(attackButtonDown);
    public void Reload() => currentWeapon.Reload();
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
    public void UpdateAmmoUI(int mag, int ammo)
    {
        magText.text = mag.ToString();
        ammoText.text = ammo.ToString();
    }
}