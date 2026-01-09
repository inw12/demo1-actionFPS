using TMPro;
using UnityEngine;
public class PlayerInteract : MonoBehaviour
{
    [Header("Raycast Lengths")]
    [SerializeField] private float closeInteractionRange;    
    [SerializeField] private float farInteractionRange;     
    [SerializeField] [Range(0, 1)] private float visionRadius;
    [SerializeField] private LayerMask mask;            
    [Header("HUD Elements")]
    [SerializeField] private TextMeshProUGUI promptText;
    private Camera cam;

    private void Start() {
        cam = GetComponentInChildren<Camera>();
    }
    private void Update()
    {
        UpdateHUD(string.Empty);
        ScanForProximity();
        ScanForVision();
    }
    private void ScanForProximity()
    {
        Ray ray = new(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * closeInteractionRange);
        if (Physics.Raycast(ray, out RaycastHit hit, closeInteractionRange, mask) && hit.collider.TryGetComponent(out Interactable interactable)) {
            UpdateHUD(interactable.promptMessage);
        }
    }
    private void ScanForVision()
    {
        // ALL collisions detected by sphere
        Collider[] hits = Physics.OverlapSphere(
            cam.transform.position,
            farInteractionRange,
            mask
        );

        // Filter through every collision detected within the sphere
        float bestDot = -1f;
        foreach (var hit in hits)
        {
            // ignore if object is not 'Interactable'
            if (!hit.TryGetComponent(out Interactable interactable)) {
                continue;
            }
            if (interactable.interactionType != Interactable.InteractionType.Vision) {
                continue;
            }
            
            // draw vision cone
            Vector3 targetDirection = (hit.transform.position - cam.transform.position).normalized;
            float dot = Vector3.Dot(cam.transform.forward, targetDirection);

            // if object is within ideal range
            if (dot > visionRadius && dot > bestDot)
            {
                bestDot = dot;
                UpdateHUD(interactable.promptMessage);
            }
        }
    }
    private void UpdateHUD(string text) => promptText.text = text;
}
