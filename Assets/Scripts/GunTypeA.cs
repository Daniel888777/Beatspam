using UnityEngine;

public class GunTypeA : MonoBehaviour
{
    private ProjectileSpawner projectileSpawner;
    private int shotCount = 8;

    void Start()
    {
        projectileSpawner = FindFirstObjectByType<ProjectileSpawner>();
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
