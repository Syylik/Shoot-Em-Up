using UnityEngine;

public class Minion : Enemy
{
    private Pool<Minion> _pool;

    public void Init(Transform afterSpawn, Pool<Minion> pool)
    {
        base.Init(afterSpawn);
        _pool = pool;

        var shoot = GetComponent<Shoot>();
        _speed = _speed + Random.Range(-0.15f, 1.2f);
        shoot.shootTime = shoot.shootTime + Random.Range(-0.15f, 0.65f);
    }

    protected override void OnDie()
    {
        base.OnDie();
        _pool.Despawn(this);
    }
}
