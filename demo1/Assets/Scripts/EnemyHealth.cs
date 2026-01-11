using UnityEngine;
public class EnemyHealth : MonoBehaviour
{
    public Color hitColor;
    public float lerpSpeed;
    [SerializeField] private float health;
    private float currentHealth;
    private Material material;
    private Color defaultColor;

    private void Start() {
        currentHealth = health;
        material = GetComponent<MeshRenderer>().material;
        material.EnableKeyword("_EMISSION");
        defaultColor = new(0, 0, 0);
    }
    private void Update()
    {
        Color current = material.GetColor("_EmissionColor");
        Color next = Color.Lerp(current, defaultColor, Time.deltaTime * lerpSpeed);
        material.SetColor("_EmissionColor", next);
    }
    public void Damage(float amount) {
        currentHealth -= amount;
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