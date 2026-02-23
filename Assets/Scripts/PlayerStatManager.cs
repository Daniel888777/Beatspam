using UnityEngine;
using TMPro;

public class PlayerStatManager : MonoBehaviour, IDamageable
{
    private AudioManager audioManager;
    [SerializeField]private int health = 10;
    [SerializeField]private TextMeshProUGUI scoreText;
    [SerializeField]private TextMeshProUGUI healthText;
    private int beatCount;
    private int score;

    private void Awake()
    {
      
        beatCount = 0;
        score = 0;
    }


    public void ScoreIncrease()
    {
        score += 5;
        scoreText.text = "Score: " + score;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        score -= 1000;
        healthText.text = "Health: " + health;
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
