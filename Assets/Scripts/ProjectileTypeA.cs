using UnityEngine;

public class ProjectileTypeA : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float damage = 1;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime); 

    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            Vector2 dir = transform.up;
            Vector2 nextPos = rb.position + (Vector2)(dir * speed * Time.deltaTime);
            rb.MovePosition(nextPos);

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
