// *** Framework for how ALL weapons should be implemented ***
// * Implementation for basic weapon functionality
// * Empty 'Attack()' function that will be implemented by child classes
using UnityEngine;
public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] protected float _damage;
    [SerializeField] protected float _fireRate;
    [SerializeField] protected float _bulletSpd;
    [SerializeField] protected float _range;
    [SerializeField] [Range(0, 100)] protected int _accuracy;
    [Header("Ammunition")]
    [SerializeField] protected int _magSize;
    [SerializeField] protected int _ammoCount;
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected Transform _bulletSpawn;
    protected int _currentMag;
    protected int _currentAmmo;
    // Other Variables
    protected Vector3 _targetDirection;
    protected Camera _cam;
    protected bool _attackButtonDown;

    public virtual void Initialize(Camera cam) => _cam = cam;
    protected virtual void Start()
    {
        _currentAmmo = _ammoCount;
        _currentMag = _magSize;
    }
    protected virtual void Update()
    {
        Ray ray = new(_cam.transform.position, _cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
            _targetDirection = (hitInfo.point - _bulletSpawn.position).normalized;
        }
        else {
            _targetDirection = _bulletSpawn.forward.normalized;
        }
    }
    public virtual void Attack(bool attackButtonDown) {}
    public virtual void Reload()
    {
        if (_currentAmmo > 0)
        {
            _currentAmmo -= _magSize - _currentMag;
            _currentAmmo = Mathf.Clamp(_currentAmmo, 0, _ammoCount);
            _currentMag = _magSize;
        }
    }
}