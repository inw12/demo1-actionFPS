using UnityEngine;
public class WeaponPickupInteractable : Interactable
{
    [SerializeField] private GameObject _weapon;
    [SerializeField] private PlayerWeapon _playerWeapon;

    protected override void Interaction()
    {
        _playerWeapon.UpdateWeapon(_weapon);
        Destroy(gameObject);
    }
}
