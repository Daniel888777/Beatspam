using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TurnAndCharge : MonoBehaviour
{
    private Transform playerPosition;
    [Header("Special Charge Stuff")]
    [SerializeField] private ParticleSystem chargerEffect;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Transform rocketLauncher1;
    [SerializeField] private Transform rocketLauncher2;
    
    private AudioManager audioManager;
    private ProjectileSpawner projectileSpawner;
    private bool canCharge = false;
    private bool isCharging = false;
    private float chargeLimit = 2000f;
    private float currentCharge = 0f;
    private float chargeCooldown = 5f;
    private float timeToNextCharge = 0f;

    [Header("Enemy Types")]
    [SerializeField] private bool enemyTypeA;
    [SerializeField] private bool enemyTypeB;
    [SerializeField] private bool enemyTypeC;

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
        if (Time.time < timeToNextCharge )
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
        if (enemyTypeA)
        {
            projectileSpawner.DischargeBlastEnemy(transform.position, transform.up);
            muzzleFlash.Play();
        }
        else if (enemyTypeB) 
        {
            Vector3 dir1 = Quaternion.Euler(0, 0, 60f) * transform.up;
            Vector3 dir2 = Quaternion.Euler(0, 0, -60f) * transform.up;
            projectileSpawner.ShootMissle(rocketLauncher1.position, rocketLauncher2.position, transform.up + dir1, transform.up + dir2);
        }
    }

    public void ChargeSound() 
    { 
        audioManager.PlaySound("EnemyCharging");
    }

    public void NoChargeSound()
    {
        audioManager.StopSound("EnemyCharging");
    }

}
