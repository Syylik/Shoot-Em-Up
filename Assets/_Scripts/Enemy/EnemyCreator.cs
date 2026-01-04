using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Enemy _bossPrefab;

    [SerializeField] private Transform _bossSpawnPos;
    [SerializeField] private List<Transform> _spawnPoses;

    [SerializeField] private float _timeBtwSpawn;
    private WaveControl _enemyControl;
    private Pool<Enemy> _enemyPool;

    private Action OnEnemySpawn, OnEnemyReduce;

    private void Awake() => _enemyPool = new Pool<Enemy>(_enemyPrefab);

    public void Init(Action onEnemySpawn, Action onEnemyReduce)
    {
        OnEnemySpawn = onEnemySpawn;
        OnEnemyReduce = onEnemyReduce;
    }

    public void SpawnEnemies(int num) => StartCoroutine(SpawnEnemiesRoutine(num, _enemyPrefab));
    public void SpawnBoss() => StartCoroutine(SpawnEnemiesRoutine(1, _bossPrefab, _bossSpawnPos));

    private IEnumerator SpawnEnemiesRoutine(int num, Enemy enemyPrefab)
    {
        while(Pause.Instance.isPaused) yield return null;

        if(num > _spawnPoses.Count) num = _spawnPoses.Count;
        var spawnPoses = new List<Transform>();
        spawnPoses = _spawnPoses.GetRange(0, _spawnPoses.Count);
        for(int i = 0; i < num; i++)
        {
            var spawn = spawnPoses[Random.Range(0, spawnPoses.Count)];
            CreateEnemy(enemyPrefab, spawn);
            spawnPoses.Remove(spawn);
            yield return new WaitForSeconds(_timeBtwSpawn);
        }
    }
    private IEnumerator SpawnEnemiesRoutine(int num, Enemy enemyPrefab, Transform spawnPos)
    {
        while(Pause.Instance.isPaused) yield return null;

        for(int i = 0; i < num; i++)
        {
            CreateEnemy(enemyPrefab, spawnPos);
            yield return new WaitForSeconds(_timeBtwSpawn);
        }
    }

    private void CreateEnemy(Enemy enemyPrefab, Transform afterSpawn)
    {
        var pos = new Vector2
            (afterSpawn.position.x + Random.Range(-0.5f, 0.5f),
            afterSpawn.position.y + Random.Range(6.5f, 10f));
        var enemy = _enemyPool.Spawn(pos, _enemyPrefab.transform.rotation);
        OnEnemySpawn?.Invoke();
        enemy.Init(afterSpawn, OnEnemyReduce, _enemyPool);
    }
}