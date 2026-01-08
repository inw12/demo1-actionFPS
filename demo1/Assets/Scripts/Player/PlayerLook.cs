// *** Implementation of player's ability to look around with the mouse ***
using UnityEngine;
public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float xSensitivity = 100f;
    [SerializeField] private float ySensitivity = 100f;
    public Camera cam;
    private float xRotation;

    private void Start() => Cursor.lockState = CursorLockMode.Locked;
    public void Look(Vector2 input)
    {
        // Vertical Rotation (camera)
        xRotation -= input.y * ySensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80, 80);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // Horizontal Rotation (camera + player object)
        transform.Rotate(input.x * Time.deltaTime * xSensitivity * Vector3.up);
    }
}
