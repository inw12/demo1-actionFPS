// *** Effect: Object oscillates up & down ***
using UnityEngine;
public class VerticalOscillation : MonoBehaviour
{
    [SerializeField] private float height; // amplitude
    [SerializeField] private float speed; // frequency 
    private float initialY;

    private void Start() => initialY = transform.position.y;
    private void Update()
    {
        float y = initialY + Mathf.Sin(speed * Time.time) * height;
        Vector3 pos = transform.position;
        pos.y = y;
        transform.position = pos;
    }
}
