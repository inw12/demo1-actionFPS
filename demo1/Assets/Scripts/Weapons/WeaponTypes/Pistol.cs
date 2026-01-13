// *** Attack implementation for 'Weapon' objects ***
using UnityEngine;
[RequireComponent(typeof(WeaponAccuracy), typeof(WeaponRecoil), typeof(WeaponEffects))]
public class Pistol : Weapon
{
    private WeaponRecoil _recoilController;
    private WeaponEffects _effectsController;    
    private WeaponAccuracy _accuracyController;
    private float _fireTimer;

    private void Start()
    {
        _recoilController = GetComponent<WeaponRecoil>();
        _accuracyController = GetComponent<WeaponAccuracy>();
        _effectsController = GetComponent<WeaponEffects>();
        _fireTimer = 0f;
    }
    protected override void Update()
    {
        base.Update();
        _fireTimer += Time.deltaTime;
    }
    protected override void WeaponAttack()
    {
        if (_fireTimer >= _fireRate)
        {
            // visual + audio effects
            _recoilController.TriggerRecoil();
            _effectsController.PlayAudio_Shoot();
            _effectsController.MuzzleFlash(_bulletSpawn.position, transform.rotation);
            // spawn bullet
            Bullet newBullet = Instantiate(_bulletPrefab, _bulletSpawn.position, transform.rotation).GetComponent<Bullet>();
            newBullet.Initialize(_damage, _bulletSpd, _range, _accuracyController.ApplyAccuracy(_targetDirection, _accuracy));
            // update variables
            _fireTimer = 0f;
        }
    }
}