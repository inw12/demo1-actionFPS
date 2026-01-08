// *** Effect: Rotate object along the y-axis ***
using UnityEngine;
public class RotateY : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    private void Update() => transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.World);
}