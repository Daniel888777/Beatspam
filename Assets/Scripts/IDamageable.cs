using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
    void HitEffect(Vector3 position);
}