using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefabA;
    [SerializeField] private GameObject projectilePrefabB;
    [SerializeField] private GameObject dichargeBlastPrefab;
    [SerializeField] private GameObject misslePrefab;
    [SerializeField] private GameObject missleExplosion;

    [SerializeField] private GameObject normalHitEffect;
    [SerializeField] private float laserRange = 100f;
    [SerializeField] private float laserDamage = 100f;
    [SerializeField] private LineRenderer laserRanderer;
    private LayerMask hittables;

    [Header("Enemy Types")]
    [SerializeField] private bool enemyTypeA;
    [SerializeField] private bool enemyTypeB;
    [SerializeField] private bool enemyTypeC;
    private AudioManager audioManager;



    void Start()
    {
        hittables = LayerMask.GetMask("EnemyLayer");
        //laserRanderer.SetPosition(0, Vector3.zero);
        //laserRanderer.SetPosition(1, Vector3.zero);
        laserRanderer.enabled = false;
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    public void RingShot(Vector3 position, Vector3 direction, int projectileCount)
    {
        float angleStep = 360f / projectileCount;
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep;

            Vector3 shotDirection = Quaternion.Euler(0f, 0f, angle) * direction;
            if (enemyTypeA)
            {
                GameObject projectile = Instantiate(projectilePrefabA, position, Quaternion.identity);
                projectile.transform.up = shotDirection;
            }
            else if (enemyTypeB)
            {
                GameObject projectile = Instantiate(projectilePrefabB, position, Quaternion.identity);
                projectile.transform.up = shotDirection;
            }
        }
    }


    public void ShootMissle(Vector3 position1, Vector3 position2, Vector3 direction1, Vector3 direction2) 
    {
        GameObject missle = Instantiate(misslePrefab, position1, Quaternion.identity);
        missle.transform.up = direction1;
        GameObject missle2 = Instantiate(misslePrefab, position2, Quaternion.identity);
        missle2.transform.up = direction2;

    }

    public void HitByProjectile(Vector3 position)
    {
        GameObject projectileExp = Instantiate(normalHitEffect, position, Quaternion.identity);
        Destroy(projectileExp, 1f);
    }
    public void DischargeBlastEnemy(Vector3 position, Vector3 direction)
    {
        Vector3 shotDirection = direction.normalized;
        GameObject dischargeBlast = Instantiate(dichargeBlastPrefab, position, Quaternion.identity);
        dischargeBlast.transform.up = shotDirection;
        audioManager.PlayShortSound("DischargeBlast");

    }

    public void MissleExplosion(Vector3 position)
    {
        GameObject explosion = Instantiate(missleExplosion, position, Quaternion.identity);
        Destroy(explosion, 2f);
    }

    public void LaserBeam(Vector3 position, Vector3 direction)
    {

        RaycastHit2D hit = Physics2D.Raycast(position, direction, laserRange, hittables);
        laserRanderer.enabled = true;
        if (hit.collider != null)
        {
            EnemyStatManager target = hit.transform.GetComponent<EnemyStatManager>();
            if (target != null)
            {
                float damagePerSec = laserDamage * Time.deltaTime; 
                target.TakeDamage(damagePerSec); 
            }
            
            MissleScript altTarget = hit.transform.GetComponent<MissleScript>();
            if (altTarget != null)
            {
                float damagePerSec = laserDamage * Time.deltaTime;
                altTarget.TakeDamage(damagePerSec);
            }
            
            
            laserRanderer.SetPosition(0, position);
            laserRanderer.SetPosition(1, hit.point);

        }
        else
        {
            laserRanderer.SetPosition(0, position);
            laserRanderer.SetPosition(1, position + direction.normalized * laserRange);
        }
        audioManager.PlaySound("LaserBeam");
    }



    public void StopLaserBeam()
    {
        //laserRanderer.SetPosition(0, Vector3.zero);
        //laserRanderer.SetPosition(1, Vector3.zero);
        laserRanderer.enabled = false;
        audioManager.StopSound("LaserBeam");

    }
}
