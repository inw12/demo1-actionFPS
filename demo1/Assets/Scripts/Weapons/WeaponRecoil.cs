using UnityEngine;
public class WeaponRecoil : MonoBehaviour
{
    [Header("Recoil Amount")]   
    [SerializeField] private float horizontalRecoil;
    [SerializeField] private float verticalRecoil;
    [SerializeField] private float recoilSpeed;
    private Quaternion _originalRotation;
    private Quaternion _targetRotation;
    private Vector3 _originalPosition;
    private Vector3 _targetPosition;
    private bool weaponFired;
    private float _recoilTimer;

    private void Start()
    {
        // weapon position + rotation
        _originalRotation = transform.localRotation;
        _originalPosition = transform.localPosition;
        _targetRotation = Quaternion.Euler(-verticalRecoil, 0, 0);
        _targetPosition = new(0, 0, -horizontalRecoil);
        // misc variables
        weaponFired = false;
        _recoilTimer = 0f;
    }
    private void Update() => ApplyRecoil();
    public void TriggerRecoil() => weaponFired = true;
    private void ApplyRecoil()
    {
        _recoilTimer += Time.deltaTime;
        float p = _recoilTimer * recoilSpeed;
        Quaternion targetRotation = weaponFired ? _targetRotation : _originalRotation;
        Vector3 targetPosition = weaponFired ? _targetPosition : _originalPosition;
        transform.SetLocalPositionAndRotation(Vector3.Lerp(transform.localPosition, targetPosition, p), Quaternion.Lerp(transform.localRotation, targetRotation, p));
        if (p > 1) {
            weaponFired = false;
            _recoilTimer = 0f;
        }
    }
}
