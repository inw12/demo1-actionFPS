using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    private float _damage;
    private float _bulletSpeed;
    private float _range;
    private Vector3 _origin;
    private Vector3 _direction;
    private Vector3 _displacement;
    private float _distanceTraveled;

    public void Initialize(float damage, float bulletSpeed, float range, Vector3 direction)
    {
        // Bullet Stats
        _damage = damage;
        _bulletSpeed = bulletSpeed;
        _range = range;
        _direction = direction;
    }
    private void Start() {
        _origin = transform.position;
    }
    private void Update()
    {
        float distanceThisFrame = _bulletSpeed * Time.deltaTime;
        if (Physics.Raycast(transform.position, _direction, out RaycastHit hitInfo, distanceThisFrame, mask))
        {
            HitEffect(hitInfo);
            Destroy(gameObject);
            return;
        }
        transform.position += _direction * distanceThisFrame;
        RangeCheck();
    }
    private void HitEffect(RaycastHit hitInfo)
    {
        Collider other = hitInfo.collider;
        if (other.CompareTag("Enemy")) {
            other.GetComponent<EnemyHealth>().Damage(_damage);
        }
        Destroy(gameObject);
    }
    private void RangeCheck()   // destroy bullet after travelling a certain distance
    {
        _displacement = transform.position - _origin;
        _distanceTraveled = Vector3.Dot(_displacement, _direction);
        if (_distanceTraveled >= _range) {
            Destroy(gameObject);
        }
    }
}