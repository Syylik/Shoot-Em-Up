using UnityEngine;

public class Miniboss : Enemy
{
    protected override void OnDie()
    {
        base.OnDie(); 
        Destroy(gameObject);
    }
}
