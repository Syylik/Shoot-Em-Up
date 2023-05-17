using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Shoot), typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _afterSpawnPoint; //Точка, в которую прилетает враг после спавна

    public UnityEvent OnMoveToSpawnStart;
    public UnityEvent OnMoveToSpawnEnd;

    private bool _isMovingToSpawn;

    private void Awake()
    {
        _isMovingToSpawn = true;
        OnMoveToSpawnStart?.Invoke();
    }

    public void Init(Transform afterSpawn, GameControl enemyControl)
    {
        _afterSpawnPoint = afterSpawn;
        _isMovingToSpawn = true;
        GetComponent<EnemyHealth>().OnDie.AddListener(enemyControl.ReduceEnemyCount);
        GetComponent<Shoot>().shootTime += Random.Range(-0.15f, 0.65f);
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
}
