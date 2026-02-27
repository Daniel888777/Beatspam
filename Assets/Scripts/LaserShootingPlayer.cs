using UnityEngine;
using UnityEngine.InputSystem;

public class LaserShootingPlayer : MonoBehaviour
{
 
    private ProjectileSpawner spawner;
    private bool canShoot = false;
    private bool discharging = false;
    [SerializeField]private Transform gunPosition;
    private BeatBar beatBar;

    void Start()
    {
        spawner = FindFirstObjectByType<ProjectileSpawner>();
        beatBar = FindFirstObjectByType<BeatBar>();

    }
    public void LaserFullyCharged()
    {
        canShoot = true;
    }
     public void LaserEmpty()
    {
        canShoot = false;
        discharging = false;
        spawner.StopLaserBeam();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canShoot)
            {
                discharging = true;
            }
        }
    }

   void Update()
    {
        if (discharging)
        {
            spawner.LaserBeam(gunPosition.position, gunPosition.up);
            beatBar.ConsumeBeat();
        }
   }



}
