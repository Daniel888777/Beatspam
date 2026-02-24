using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TurnAndCharge : MonoBehaviour
{
    private Transform playerPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPosition = FindFirstObjectByType<PlayerStatManager>().transform;
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

}
