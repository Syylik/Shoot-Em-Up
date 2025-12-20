using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Shoot), typeof(Health))]
public class Enemy : MonoBehaviour, IPoolObject
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _afterSpawnPoint;

    public UnityEvent OnMoveToSpawnStart;
    public UnityEvent OnMoveToSpawnEnd;

    [SerializeField] private EnemyHealth _health;

    private bool _isMovingToSpawn;
    private GameControl _enemyControl;
    private Pool<Enemy> _pool;

    private void Awake()
    {
        _isMovingToSpawn = true;
        OnMoveToSpawnStart?.Invoke();
    }

    public void Init(Transform afterSpawn, GameControl enemyControl, Pool<Enemy> pool)
    {
        _afterSpawnPoint = afterSpawn;
        _isMovingToSpawn = true;
        _enemyControl = enemyControl;
        _pool = pool; 
        _health.OnDie.AddListener(OnDie);
        var shoot = GetComponent<Shoot>();
        shoot.shootTime = shoot.shootTime + Random.Range(-0.15f, 0.65f);
        OnMoveToSpawnStart?.Invoke();
    }

    private void Update()
    {
        if(Pause.Instance.isPaused) return;

        if(_isMovingToSpawn)
        {
            transform.position = Vector2.MoveTowards(transform.position, _afterSpawnPoint.position, _speed * Time.deltaTime);
            if(transform.position == _afterSpawnPoint.position)
            {
                _isMovingToSpawn = false;
                OnMoveToSpawnEnd?.Invoke();
            }
        }
    }

    private void OnDie()
    {
        _enemyControl.ReduceEnemyCount();
        _pool.Despawn(this);
    }

    public void Enable(Vector2 position, Quaternion rotation)
    {
        _health.OnDie.RemoveListener(OnDie);
        _health.health = _health.maxHealth;
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}