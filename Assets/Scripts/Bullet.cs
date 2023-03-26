using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private GameObject _damageEffect;

    private void Start() => Destroy(gameObject, 1.5f);

    public void SetOwnerLayer(LayerMask ownerMask) => gameObject.layer = ownerMask;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<Health>(out Health health))
        {
            health.ChangeHealth(-_damage);
            if(health.canTakeHit) Destroy(Instantiate(_damageEffect, transform.position, Quaternion.identity), 1.5f);
            Destroy(gameObject);
        }
    }
}