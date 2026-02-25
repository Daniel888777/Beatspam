using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject dichargeBlastPrefab;



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
    
    public void DischargeBlast(Vector3 position, Vector3 direction)
    {
        Vector3 shotDirection = direction.normalized;
        GameObject dischargeBlast = Instantiate(dichargeBlastPrefab, position, Quaternion.identity);
        dischargeBlast.transform.up = shotDirection;

    }
}
