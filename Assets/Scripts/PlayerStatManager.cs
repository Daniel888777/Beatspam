using UnityEngine;
using TMPro;

public class PlayerStatManager : MonoBehaviour, IDamageable
{
    private AudioManager audioManager;
    [SerializeField]private float maxHealth = 20f;
    private float health;
    [SerializeField]private TextMeshProUGUI scoreText;
    //[SerializeField]private TextMeshProUGUI healthText;
    private HealthBar healthBar;
    private BeatBar beatBar;
    private int beatCount;
    private int score;

    private void Awake()
    {
        healthBar = FindFirstObjectByType<HealthBar>();
        beatBar = FindFirstObjectByType<BeatBar>();
        beatCount = 0;
        score = 0;
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        health = maxHealth;
    }

    public void ScoreIncrease()
    {
        score += 5;
        beatBar.IncreaseBeatEnergy();
        

        scoreText.text = "Score: " + score;

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.setCurrentHealth(health);
        score -= 1000;
        //healthText.text = "Health: " + health;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        Destroy(gameObject);
    }
}
