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
    private ProjectileSpawner projectileSpawner;
    [SerializeField]private GameObject shieldEffect;
    private int score;
    private bool shieldHasEnergy = true;
    private float shieldDuration = 2f;
    private float shieldTimer = 0f;

    private void Awake()
    {
        scoreText.text = "Score: " + score;    
        shieldEffect.SetActive(false);
        projectileSpawner = FindFirstObjectByType<ProjectileSpawner>();
        healthBar = FindFirstObjectByType<HealthBar>();
        beatBar = FindFirstObjectByType<BeatBar>();
        audioManager = FindFirstObjectByType<AudioManager>();
        score = 0;
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        health = maxHealth;
    }

    void Update() 
    { 
        if (Time.time < shieldTimer && shieldHasEnergy) 
        {   
            shieldEffect.SetActive(true);

        }
        else if (!shieldHasEnergy || Time.time >= shieldTimer)
        {
             shieldEffect.SetActive(false);
        }
    }
    public void ScoreIncrease()
    {
        score += 5;
        beatBar.IncreaseBeatEnergy();
        ScoreManager.Instance.AddScore(5);
        scoreText.text = "Score: " + score;

    }

    public void TakeDamage(float damage)
    {


        if (health > 10 &&shieldHasEnergy)
        {
            audioManager.PlayShortSound("PlayerHit");
        }
        else if (health <= 10 && shieldHasEnergy)
        {
            shieldHasEnergy = false;
            audioManager.PlayShortSound("ShieldBreak");
        }
        else
        {
            audioManager.PlayShortSound("PlayerHitNoShield");
        }

        if (shieldHasEnergy)
        {
            health -= damage /2;
        }
        else
        {
            health -= damage;
        }

        healthBar.setCurrentHealth(health);
        score -= 1000;
        ScoreManager.Instance.AddScore(-1000);

        if (shieldHasEnergy)
        {
            shieldTimer = Time.time + shieldDuration;
        }
        //healthText.text = "Health: " + health;
        
        if (health <= 0)
        {
            Die();
        }
    }

    public void HitEffect(Vector3 position) 
    { 
        projectileSpawner.HitByProjectile(position);
    }

    private void Die()
    {
        GameStateManager.Instance.SetState(GameStates.Death);
        projectileSpawner.StopLaserBeam();
        projectileSpawner.PlayerExplosion(transform.position);
        audioManager.PlayShortSound("PlayerExplosion");
        Destroy(gameObject);
    }
}
