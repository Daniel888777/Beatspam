using UnityEngine;

public class RocketExplosion : MonoBehaviour
{
    [SerializeField] private float explosionDamage = 10f;
    private Rigidbody2D rb_;
    

    void Start()
    {
        rb_ = GetComponent<Rigidbody2D>();
        if (rb_ != null)
        {
            Debug.Log("No rb_");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(explosionDamage);
                damageable.HitEffect(transform.position);
            }
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }

}
