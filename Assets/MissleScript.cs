using UnityEngine;

public class MissleScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float damage = 1;
    [SerializeField] private float turnSpeed =50f;
    private bool active = false;
    [SerializeField]private float activationDelay = 2f;
    private float timeTilActive = 0;
    private Transform playerPosition;
    private bool lockedOn = false;
    private Vector2 targetLockOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeTilActive = Time.time + activationDelay;
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
        PlayerStatManager player = FindFirstObjectByType<PlayerStatManager>();

        if (player != null)
        {
            playerPosition = player.transform;
        }
        else
        {
            Debug.LogWarning("PlayerStatManager not found in scene!");
        }
    }

    private void Update()
    {
        if (Time.time >= timeTilActive)
        {
            active = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb != null)
        {
            if (!active)
            {
                Vector2 dir = transform.up;
                Vector2 nextPos = rb.position + (Vector2)(dir * speed * Time.fixedDeltaTime);
                rb.MovePosition(nextPos);
            }
            else if (playerPosition != null)
            {
                //Vector2 targetPosition = playerPosition.position;
                //if (!lockedOn)
                //{
                //    targetLockOn = Random.insideUnitCircle * 1f + targetPosition;
                //    lockedOn = true;
                //}
                //Vector2 directionToTarget = targetLockOn  - (Vector2)transform.position;
                //directionToTarget.Normalize();
                //float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                //angle -= 90f;
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.fixedDeltaTime * turnSpeed);
                //Vector2 nextPos = rb.position + (Vector2)transform.up * speed * Time.fixedDeltaTime;
                //rb.MovePosition(nextPos);

                Vector2 targetPosition = playerPosition.position;
                Vector2 directionToTarget = targetPosition - (Vector2)transform.position;
                directionToTarget.Normalize();
                float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                angle -= 90f;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.fixedDeltaTime * turnSpeed);
                Vector2 nextPos = rb.position + (Vector2)transform.up * speed * Time.fixedDeltaTime;
                rb.MovePosition(nextPos);



            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                damageable.HitEffect(transform.position);
            }
            Destroy(gameObject);
        }
    }
}
