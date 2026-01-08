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
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected Transform _bulletSpawn;
    // Other Variables
    protected Vector3 _targetDirection;
    protected Camera _cam;
    protected bool _attackButtonDown;

    public virtual void Initialize(Camera cam) => _cam = cam;
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
    public void Attack(bool attackButtonDown)
    {
        _attackButtonDown = attackButtonDown;
        WeaponAttack();
    }
    protected virtual void WeaponAttack() {}
}