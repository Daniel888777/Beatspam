using UnityEngine;

public class EnemyStatManager : MonoBehaviour, IDamageable
{
    [SerializeField]private float maxHealth = 100f;
    private float health;
    private EnemyHealthBar healthBar;
    private TurnAndCharge turnAndCharge;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        turnAndCharge = GetComponent<TurnAndCharge>();
        healthBar = FindFirstObjectByType<EnemyHealthBar>();
    }

    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.setCurrentHealth(health);
        Debug.Log("Enemy took damage: " + damage + ", current health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        StartCoroutine(DeathEffect());
    }

    public void HitEffect(Vector3 position)
    {
        
    }


    private System.Collections.IEnumerator DeathEffect()
    {
        // Add any death animation or effect here
        yield return new WaitForSeconds(0.5f); // Wait for the effect to finish
        GameStateManager.Instance.OnPlayerVictory();
        turnAndCharge.NoChargeSound();
        Destroy(gameObject);

    }
}
