using UnityEngine;

public class GunTypeA : MonoBehaviour
{
    private ProjectileSpawner projectileSpawner;
    private int shotCount = 8;
    [SerializeField] private Transform enemyPosition;

    void Start()
    {
        transform.position = enemyPosition.position;
        projectileSpawner = FindFirstObjectByType<ProjectileSpawner>();
    }

    private void Update()
    {
        transform.position = enemyPosition.position;

    }
    public void TurnGun()
    {
        transform.Rotate(0f, 0f, 31f);
    }
    
    public void Fire()
    {
        projectileSpawner.RingShot(transform.position, transform.up, shotCount);
    }
}
