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
    [Header("Damage Feedback | Particle Emission")]
    public GameObject hitParticles;
    public GameObject deathParticles;
    public float deathSpeed;
    private bool isAlive;
    private bool deathEffectTriggered = false;
    [Header("Damage Feedback | SFX")]
    public AudioClip hitSound;
    private AudioSource src;
    [Header("Damage Feedback | Death Explosion")]
    public float deathForce = 4f;
    public float deathRadius = 1f;
    public float upwardBoost = 0.5f;

    private void Start() {
        currentHealth = health;
        material = GetComponent<MeshRenderer>().material;
        material.EnableKeyword("_EMISSION");
        baseScale = transform.localScale;
        defaultColor = new(0, 0, 0);
        isAlive = true;
        src = GetComponent<AudioSource>();
    }
    private void Update()
    {
        // Emission Lerp
        Color current = material.GetColor("_EmissionColor");
        Color next = Color.Lerp(current, defaultColor, Time.deltaTime * effectSpeed);
        material.SetColor("_EmissionColor", next);
        // Scale Lerp
        if (isAlive)
        {
            // Return to normal scale
            scaleOffset = Vector3.Lerp(scaleOffset, Vector3.zero, Time.deltaTime * growSpeed);
            transform.localScale = baseScale + scaleOffset;
        }
        else
        {
            // Shrink to nothingness
            scaleOffset = Vector3.Lerp(scaleOffset, Vector3.one, Time.deltaTime * deathSpeed);
            transform.localScale = baseScale - scaleOffset;
            if (transform.localScale.y < 0.25f)
            {
                if (!deathEffectTriggered)
                {
                    deathEffectTriggered = true;
                    _ = Instantiate(deathParticles, transform.position, Quaternion.identity);
                    DeathExplosion();
                    Destroy(gameObject, 1f);
                }
            }
        }
    }
    public void Damage(float amount, Vector3 point, Vector3 normal) {
        currentHealth -= amount;
        isAlive = currentHealth > 0;
        // Audio
        if (src) src.PlayOneShot(hitSound);
        // Enemy Pulse 
        scaleOffset = Vector3.one * -pulseAmount;
        // Glow
        material.SetColor("_EmissionColor", hitColor * hitBrightness);
        // Particle Spawn
        _ = Instantiate(hitParticles, point, Quaternion.LookRotation(normal));
    }
    private void DeathExplosion()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, deathRadius);
        foreach (Collider hit in hits)
        {
            Rigidbody rb = hit.attachedRigidbody;
            if (rb == null) continue;

            rb.AddExplosionForce(
                deathForce,
                transform.position,
                deathRadius,
                upwardBoost,
                ForceMode.Impulse
            );
        }
    }
}