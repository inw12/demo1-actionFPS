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
    [Header("Damage Feedback | Death")]
    public float deathSpeed = 10f;
    private bool isAlive;

    private void Start() {
        currentHealth = health;
        material = GetComponent<MeshRenderer>().material;
        material.EnableKeyword("_EMISSION");
        baseScale = transform.localScale;
        defaultColor = new(0, 0, 0);
    }
    private void Update()
    {
        isAlive = currentHealth > 0;
        // Emission color -> Default
        Color current = material.GetColor("_EmissionColor");
        Color next = Color.Lerp(current, defaultColor, Time.deltaTime * effectSpeed);
        material.SetColor("_EmissionColor", next);
        // Scale -> Default
        if (isAlive)
        {
            scaleOffset = Vector3.Lerp(scaleOffset, Vector3.zero, Time.deltaTime * growSpeed);
            transform.localScale = baseScale + scaleOffset;
        }
        // Scale -> Zero
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * deathSpeed);
            if (transform.localScale.y < 0.1) {
                Destroy(gameObject);
            }
        }
    }
    public void Damage(float amount) {
        // reduce HP
        currentHealth -= amount;
        // visual hit feedback
        scaleOffset = Vector3.one * -pulseAmount;
        material.SetColor("_EmissionColor", hitColor * hitBrightness);
    }
}