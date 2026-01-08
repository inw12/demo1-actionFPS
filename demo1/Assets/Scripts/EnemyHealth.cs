using UnityEngine;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    private float currentHealth;

    private void Start() => currentHealth = health;
    public void Damage(float amount) {
        Debug.Log("Hit!");
        currentHealth -= amount;
        CheckForDeath();
    }
    private void CheckForDeath()
    {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}