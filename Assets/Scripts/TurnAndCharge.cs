using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TurnAndCharge : MonoBehaviour
{
    private Transform playerPosition;
    [SerializeField] private ParticleSystem chargerEffect;
    private AudioManager audioManager;
    private ProjectileSpawner projectileSpawner;
    private bool canCharge = false;
    private bool isCharging = false;
    private float chargeLimit = 2000f;
    private float currentCharge = 0f;
    private float chargeCooldown = 5f;
    private float timeToNextCharge = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPosition = FindFirstObjectByType<PlayerStatManager>().transform;
        audioManager = FindFirstObjectByType<AudioManager>();
        projectileSpawner = FindFirstObjectByType<ProjectileSpawner>();
    }



    public void TurnTowardsPlayer()
    {
        if (playerPosition != null)
        {
            Vector3 directionToPlayer = playerPosition.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            angle -= 90f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 50f);
            //if (Mathf.Abs(transform.rotation.eulerAngles.z - angle) <= 0.5f)
            //{ }
        }
    }


    public void EnableCharge()
    {
        canCharge = true;
    }

    public void Charging() { 
        if (Time.time < timeToNextCharge)
        {
            return;
        }
        if (canCharge)
        {   
            isCharging = true;
            currentCharge += 50f;
            ChargeSound();


            if (!chargerEffect.isPlaying)
            {
                chargerEffect.Play();
            }

        }
        if (currentCharge >= chargeLimit)
        {
            //Trigger the charge attack here
            Debug.Log("Charge Attack Triggered!");
            isCharging = false;
            currentCharge = 0f;
            chargerEffect.Stop();
            NoChargeSound();


            timeToNextCharge = Time.time + chargeCooldown;
            Discharge();
        }
    }


    private void Discharge()
    {
        projectileSpawner.DischargeBlastEnemy(transform.position, transform.up);
    }

    public void ChargeSound() 
    { 
        audioManager.PlayChargeSound("EnemyCharging");
    }

    public void NoChargeSound()
    {
        audioManager.StopChargeSound("EnemyCharging");
    }

}
