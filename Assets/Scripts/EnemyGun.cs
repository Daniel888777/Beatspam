using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    private ProjectileSpawner projectileSpawner;
    [SerializeField]private int shotCount = 8;
    [SerializeField] private Transform enemyPosition;
    [Header("Enemy Types")]
    [SerializeField] private bool enemyTypeA;
    [SerializeField] private bool enemyTypeB;
    [SerializeField] private bool enemyTypeC;

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
