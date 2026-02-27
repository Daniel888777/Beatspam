using UnityEngine;

public class GunTypeA : MonoBehaviour
{
    private ProjectileSpawner projectileSpawner;
    [SerializeField]private int shotCount = 8;
    [SerializeField] private Transform enemyPosition;

    void Start()
    {
        transform.position = enemyPosition.position;
        projectileSpawner = FindFirstObjectByType<ProjectileSpawner>();
    }

    private void Update()
    {
        if (enemyPosition != null)
        {
            transform.position = enemyPosition.position;
        }
        

    }
    public void TurnGun()
    {
        transform.Rotate(0f, 0f, 37f);
    }
    
    public void Fire()
    {
        projectileSpawner.RingShot(transform.position, transform.up, shotCount);
    }
}
