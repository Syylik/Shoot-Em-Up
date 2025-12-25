using UnityEngine;

public class EnemyHealth : Health
{
    public override void Die()
    {
        base.Die();
        // Destroy(gameObject);
    }
}
