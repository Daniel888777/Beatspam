using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject dichargeBlastPrefab;
    [SerializeField] private GameObject normalHitEffect;
    [SerializeField] private float laserRange = 100f;
    [SerializeField] private float laserDamage = 100f;
    [SerializeField] private LineRenderer laserRanderer;
    private LayerMask hittables;



    void Start()
    {
        hittables = LayerMask.GetMask("EnemyLayer");
        //laserRanderer.SetPosition(0, Vector3.zero);
        //laserRanderer.SetPosition(1, Vector3.zero);
        laserRanderer.enabled = false;
    }

    public void RingShot(Vector3 position, Vector3 direction, int projectileCount)
    {
        float angleStep = 360f / projectileCount;
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep;

            Vector3 shotDirection = Quaternion.Euler(0f, 0f, angle) * direction;

            GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.identity);

            projectile.transform.up = shotDirection;
        }
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
            laserRanderer.SetPosition(0, position);
            laserRanderer.SetPosition(1, hit.point);
        }
        else
        {
            laserRanderer.SetPosition(0, position);
            laserRanderer.SetPosition(1, position + direction.normalized * laserRange);
        }
    }

    public void StopLaserBeam()
    {
        //laserRanderer.SetPosition(0, Vector3.zero);
        //laserRanderer.SetPosition(1, Vector3.zero);
        laserRanderer.enabled = false;

    }
}
