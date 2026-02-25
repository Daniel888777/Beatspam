using UnityEngine;

public class DischargeBlast : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float lifetime = 6f;
    [SerializeField] private int damage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime); // Destroy the projectile after 5 seconds to prevent memory leaks

    }

    // Update is called once per frame
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
            IDamageable damageable = collision.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
