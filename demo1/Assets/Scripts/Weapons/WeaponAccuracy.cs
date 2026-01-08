using UnityEngine;
public class WeaponAccuracy : MonoBehaviour
{
    [SerializeField] [Range(1, 10)] private int accuracyStrength;
    public Vector3 ApplyAccuracy(Vector3 targetDirection, float accuracy)
    {
        float d = (100f - accuracy) / 100f / accuracyStrength;
        // random xy values
        Vector3 offset = new(
            Random.Range(-d, d),
            Random.Range(-d, d),
            0f
        );
        // x & y vectors perpendicular to our 'targetDirection' (z)
        Vector3 right = Vector3.Cross(targetDirection, Vector3.up).normalized;
        Vector3 up = Vector3.Cross(right, targetDirection).normalized;
        // apply offset to those x & y vectors
        Vector3 finalOffset = (right * offset.x) + (up * offset.y);
        // apply offset to original vector
        return (targetDirection + finalOffset).normalized;
    }
}