using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;



    public void RingShot(Vector3 position, Vector3 direction, int projectileCount)
    {
        float angleStep = 360f / projectileCount;
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep;

            Vector3 shotDirection =
                Quaternion.Euler(0f, 0f, angle) * direction;

            GameObject projectile =
                Instantiate(projectilePrefab, position, Quaternion.identity);

            projectile.transform.up = shotDirection;
        
        }



    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
