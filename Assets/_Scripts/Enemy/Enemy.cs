using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Shoot), typeof(Health))]
public abstract class Enemy : MonoBehaviour, IPoolObject
{
    [SerializeField] protected float _speed;
    [SerializeField] private Transform _afterSpawnPoint;

    public UnityEvent OnMoveToSpawnStart;
    public UnityEvent OnMoveToSpawnEnd;

    [SerializeField] private EnemyHealth _health;

    private bool _isMovingToSpawn;

    private void Awake()
    {
        _isMovingToSpawn = true;
        OnMoveToSpawnStart?.Invoke();
    }

    public void Init(Transform afterSpawn)
    {
        _afterSpawnPoint = afterSpawn;
        _isMovingToSpawn = true;
        
        Registry<Enemy>.TryAdd(this);
        _health.OnDie.AddListener(OnDie);
        
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

    protected virtual void OnDie() => Registry<Enemy>.Remove(this);

    public void Enable(Vector2 position, Quaternion rotation)
    {
        _health.OnDie.RemoveListener(OnDie);
        _health.health = _health.maxHealth;
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
    }

    public void Disable() => gameObject.SetActive(false);

    private void OnDestroy() => Registry<Enemy>.Remove(this);
}