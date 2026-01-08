using UnityEngine;
public class PlayerInteract : MonoBehaviour
{
    [Header("Raycast Lengths")]
    [SerializeField] private float shortRayDistance;    // objects up close
    [SerializeField] private float farRayDistance;      // objects far away
    [SerializeField] private LayerMask mask;            // interaction mask
    private Camera cam;

    private void Start() {
        cam = GetComponentInChildren<Camera>();
    }
    private void Update()
    {
        Ray ray = new(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * shortRayDistance);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, shortRayDistance, mask))
        {
            
        }
    }
}
