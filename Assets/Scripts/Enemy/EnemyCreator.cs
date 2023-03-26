using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] private GameControl _enemyControl;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private List<Transform> _spawnPoses;
    [SerializeField] private float _timeBtwSpawn;

    private void Start() => SpawnEnemies(5);

    public void SpawnEnemies(int num) => StartCoroutine(SpawnEnemiesRoutine(num));

    private IEnumerator SpawnEnemiesRoutine(int num)
    {
        if(num > _spawnPoses.Count) num = _spawnPoses.Count;
        _enemyControl.SetEnemyCount(num);
        var spawnPoses = new List<Transform>();
        spawnPoses = _spawnPoses.GetRange(0, _spawnPoses.Count);
        for(int i = 0; i < num; i++)
        {
            var spawn = spawnPoses[Random.Range(0, spawnPoses.Count)];
            CreateEnemy(spawn);
            spawnPoses.Remove(spawn);
            yield return new WaitForSeconds(_timeBtwSpawn);
        }
    }

    private void CreateEnemy(Transform afterSpawn)
    {
        var pos = new Vector2
            (afterSpawn.position.x + Random.Range(-0.5f, 0.5f),
            afterSpawn.position.y + Random.Range(6.5f, 10f));

        var enemy = Instantiate(_enemyPrefab, pos, _enemyPrefab.transform.rotation);
        enemy.Init(afterSpawn, _enemyControl);
    }
}
