using UnityEngine;
public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Health")]
    [SerializeField] private float health;
    private float currentHealth;
    
    [Header("Damage Feedback")]
    Vector3 baseScale;
    Vector3 scaleOffset;
    public float hitShrinkAmount = 0.15f;
    public float shrinkSpeed = 25f;
    public float returnSpeed = 10f;

    [SerializeField] private Color hitColor;
    [SerializeField] private float hitEffectSpeed;
    private Material material;
    private Color defaultColor;


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
        Color next = Color.Lerp(current, defaultColor, Time.deltaTime * hitEffectSpeed);

        scaleOffset = Vector3.Lerp(scaleOffset, Vector3.zero, Time.deltaTime * returnSpeed);
        transform.localScale = baseScale + scaleOffset;

        material.SetColor("_EmissionColor", next);
    }
    public void Damage(float amount) {
        currentHealth -= amount;
        scaleOffset = Vector3.one * -hitShrinkAmount;
        material.SetColor("_EmissionColor", hitColor * 5f);
        CheckForDeath();
    }
    private void CheckForDeath()
    {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}