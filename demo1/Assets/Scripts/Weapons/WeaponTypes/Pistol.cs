// *** Attack implementation for 'Weapon' objects ***
using UnityEngine;
[RequireComponent(typeof(WeaponAccuracy), typeof(WeaponRecoil), typeof(WeaponEffects))]
public class Pistol : Weapon
{
    private PlayerWeapon _weaponController;
    private WeaponRecoil _recoilController;
    private WeaponEffects _effectsController;    
    private WeaponAccuracy _accuracyController;
    private float _fireTimer;

    protected override void Start()
    {
        base.Start();
        _weaponController = GetComponentInParent<PlayerWeapon>();
        _recoilController = GetComponent<WeaponRecoil>();
        _accuracyController = GetComponent<WeaponAccuracy>();
        _effectsController = GetComponent<WeaponEffects>();
        _fireTimer = 0f;
    }
    protected override void Update()
    {
        base.Update();
        _fireTimer += Time.deltaTime;
        _weaponController.UpdateAmmoUI(_currentMag, _currentAmmo);
    }
    public override void Attack(bool attackButtonDown)
    {
        if (_currentMag > 0 && _fireTimer >= _fireRate)
        {
            // visual + audio effects
            _recoilController.TriggerRecoil();
            _effectsController.PlayMuzzleFlash(_bulletSpawn);
            _effectsController.PlayAudio_Shoot();
            // spawn bullet
            Bullet newBullet = Instantiate(_bulletPrefab, _bulletSpawn.position, transform.rotation).GetComponent<Bullet>();
            newBullet.Initialize(_damage, _bulletSpd, _range, _accuracyController.ApplyAccuracy(_targetDirection, _accuracy));
            // update variables
            _currentMag--;
            _fireTimer = 0f;
        }
    }
}