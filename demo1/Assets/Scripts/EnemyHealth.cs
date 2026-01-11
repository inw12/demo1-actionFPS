using UnityEngine;
public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Health")]
    [SerializeField] private float health;
    private float currentHealth;
    
    [Header("Damage Feedback | Emission")]
    public Color hitColor;
    public float hitBrightness;
    public float effectSpeed;
    private Material material;
    private Color defaultColor;
    [Header("Damage Feedback | Pulse")]
    public float pulseAmount = 0.15f;
    public float shrinkSpeed = 25f;
    public float growSpeed = 25f;
    private Vector3 scaleOffset;
    private Vector3 baseScale;

    private void Start() {
        currentHealth = health;
        material = GetComponent<MeshRenderer>().material;
        material.EnableKeyword("_EMISSION");
        baseScale = transform.localScale;
        defaultColor = new(0, 0, 0);
    }
    private void Update()
    {
        Color current = material.GetColor("_EmissionColor");
        Color next = Color.Lerp(current, defaultColor, Time.deltaTime * effectSpeed);

        scaleOffset = Vector3.Lerp(scaleOffset, Vector3.zero, Time.deltaTime * growSpeed);
        transform.localScale = baseScale + scaleOffset;

        material.SetColor("_EmissionColor", next);
    }
    public void Damage(float amount) {
        currentHealth -= amount;
        scaleOffset = Vector3.one * -pulseAmount;
        material.SetColor("_EmissionColor", hitColor * hitBrightness);
        CheckForDeath();
    }
    private void CheckForDeath()
    {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}